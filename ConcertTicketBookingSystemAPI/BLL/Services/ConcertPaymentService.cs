using BLL.Configurations;
using BLL.Dtos.ConcertsDtos;
using BLL.HtmlTicketTemplates;
using BLL.Interfaces;
using ConfirmationService;
using EmailSender;
using PayPal;
using Microsoft.Extensions.Options;
using PayPalCheckoutSdk.Orders;
using System;
using System.Threading.Tasks;
using PayPalHttp;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ConcertPaymentService : IConcertPaymentService
    {
        private readonly IEmailSending _senderService;
        private readonly IPaymentService<HttpResponse> _payment;
        private readonly IConfirmationService<string, (IUsersRepository, IConcertsRepository)> _confirmationService;
        private readonly IActionsRepository _actionsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IConcertsRepository _concertsRepository;
        private readonly ITicketsRepository _ticketsRepository;
        private readonly BaseLinksConf _baseLinksConf;

        public ConcertPaymentService(IEmailSending senderService,
            IConfirmationService<string, (IUsersRepository, IConcertsRepository)>
            confirmationService, IPaymentService<HttpResponse> payment,
            IActionsRepository actionsRepository,
            IUsersRepository usersRepository,
            IConcertsRepository concertsRepository,
            ITicketsRepository ticketsRepository,
            IOptions<BaseLinksConf> baseLinksConf)
        {
            _senderService = senderService;
            _baseLinksConf = baseLinksConf.Value;
            _payment = payment;
            _actionsRepository = actionsRepository;
            _usersRepository = usersRepository;
            _concertsRepository = concertsRepository;
            _ticketsRepository = ticketsRepository;
            _confirmationService = confirmationService;
        }

        public async Task ConfirmedPaymentAsync(string token, Guid userId)
        {
            HttpResponse response;
            response = await _payment.CaptureOrderAsync(token);
            Order result = response.Result<Order>();
            if (result.Status.Trim().ToUpper() == "COMPLETED")
            {
                _confirmationService.Confirm(result.Id, (_usersRepository, _concertsRepository));
                await _actionsRepository.AddActionAsync(userId, "Bought ticket");
            }
            else throw new Exception();
        }

        public async Task<string> PrePayAsync(int concertId, BuyTicketDto dto, Guid userId)
        {
            var concert = await _concertsRepository.GetByIdAsync(concertId);
            var user = await _usersRepository.GetByIdIncludingAsync(userId, u => u.PromoCode);
            var ticket = dto.ToTicket(userId, concertId, user.PromoCodeId);
            decimal cost = concert.Cost * dto.Count;
            if (user.PromoCode != null) cost = cost > user.PromoCode.Discount ? cost - user.PromoCode.Discount : 0;
            async Task BuyTicketBodyTemplateAsync()
            {
                if (user.PromoCode != null)
                {
                    ticket.PromoCodeId = user.PromoCodeId;
                    user.PromoCodeId = null;
                    user.PromoCode = null;
                }
                concert.LeftCount -= ticket.Count;
                await _ticketsRepository.AddAsync(ticket);
                await _ticketsRepository.SaveChangesAsync();
                _senderService.SendHtml("Ticket", user.Email,
                    new SimpleHtmlTicketTemplate(ticket.TicketId, ticket.Count, user.Name, cost,
                    _baseLinksConf.FrontUrl, concert.ConcertId).GetHtml(), 5);
            }
            if (cost != 0)
            {
                HttpResponse response = await _payment.CreateOrderAsync("USD", cost, "Count: " + ticket.Count + "\n");
                Order result = response.Result<Order>();
                string approveUrl = GetApproveUrl(result);

                if (!string.IsNullOrEmpty(approveUrl))
                {
                    _confirmationService.Add(result.Id, async ((IUsersRepository usersRepository, IConcertsRepository concertsRepository) repositories) =>
                    {

                        user = await repositories.usersRepository.GetByIdIncludingAsync(user.UserId, u => u.PromoCode);
                        concert = await repositories.concertsRepository.GetByIdAsync(concert.ConcertId);
                        await BuyTicketBodyTemplateAsync();
                    });
                }
                return approveUrl;
            }
            else
            {
                await BuyTicketBodyTemplateAsync();
                return null;
            }
        }
        private string GetApproveUrl(Order result)
        {
            foreach (LinkDescription link in result.Links)
            {
                if (link.Rel.Trim().ToLower() == "approve")
                {
                    return link.Href;
                }
            }
            return null;
        }
    }
}
