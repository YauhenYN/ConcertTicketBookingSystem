using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.ConcertsDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using ConcertTicketBookingSystemAPI.CustomServices;
using Microsoft.Extensions.Configuration;
using ConcertTicketBookingSystemAPI.CustomServices.EmailSending;
using ConcertTicketBookingSystemAPI.CustomServices.PayPal;
using ConcertTicketBookingSystemAPI.CustomServices.ConfirmationService;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConcertsController : ControllerBase
    {
        private readonly ILogger<ConcertsController> _logger;
        private readonly Models.ApplicationContext _context;
        private readonly EmailSenderService _senderService;
        private readonly PayPalPayment _payment;
        private readonly IConfiguration _configuration;
        private readonly IConfirmationService<Guid> _confirmationService;

        public ConcertsController(ILogger<ConcertsController> logger, Models.ApplicationContext context, EmailSenderService senderService, IConfirmationService<Guid> confirmationService, PayPalPayment payment, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _senderService = senderService;
            _payment = payment;
            _configuration = configuration;
            _confirmationService = confirmationService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Token")]
        [Route("{concertId}")]
        public async Task<ActionResult<ConcertDto>> GetConcertAsync(int concertId)
        {
            var concert = await _context.Concerts.Include(c => c.Images).FirstOrDefaultAsync(c => concertId == c.ConcertId);
            if (concert != null)
            {
                if (concert is Models.ClassicConcert) return ((Models.ClassicConcert)concert).ToDto();
                else if (concert is Models.OpenAirConcert) return ((Models.OpenAirConcert)concert).ToDto();
                else return ((Models.PartyConcert)concert).ToDto();
            }
            else return NotFound();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Token")]
        [Route("{concertId}/Buy/PayPal")]
        public async Task<ActionResult> BuyTicket_PayPalAsync(int concertId, BuyTicketDto dto)
        {
            if (_context.Concerts.Any(c => c.ConcertId == concertId))
            {
                var concert = await _context.Concerts.FirstOrDefaultAsync(c => c.ConcertId == concertId);
                var user = await _context.Users.Include(u => u.PromoCode).FirstOrDefaultAsync(u => u.UserId == Guid.Parse(HttpContext.User.Identity.Name));
                var ticket = dto.ToTicket(Guid.Parse(HttpContext.User.Identity.Name), concertId, user.PromoCodeId.Value);
                decimal cost = concert.Cost;
                if (user.PromoCode != null) cost = cost - user.PromoCode.Discount;
                HttpResponse response = await _payment.CreateOrderAsync("USD", cost, "Count: " + ticket.Count + "\n");
                Order result = response.Result<Order>();
                string approveUrl = null;
                foreach (LinkDescription link in result.Links)
                {
                    if (link.Rel.Trim().ToLower() == "approve")
                    {
                        approveUrl = link.Href;
                    }
                }
                if (!string.IsNullOrEmpty(approveUrl))
                {
                    _confirmationService.Add(Guid.Parse(HttpContext.User.Identity.Name), async () =>
                    {
                        if (user.PromoCode != null)
                        {
                            ticket.PromoCodeId = user.PromoCodeId;
                            user.PromoCodeId = null;
                        }
                        concert.LeftCount--;
                        await _context.Tickets.AddAsync(ticket);
                        await _context.SaveChangesAsync();
                        await _senderService.SendHtmlAsync("Ticket", user.Email,
                            $"<p>Id Билета - {ticket.TicketId}</p>" +
                            $"<p>На количество - {ticket.Count}</p>" +
                            $"<p>Кому - {user.Name}</p>" +
                            $"<p>Оплачено - {cost}</p>" +
                            $"<h1>На концерт:</h1>" +
                            $"<a href = \"{_configuration["RedirectUrl"]}/Concerts/{concert.ConcertId}\">Концерт</a>");
                    });
                    return Redirect(approveUrl);
                }
                else return Conflict();
            }
            else return NotFound();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Token")]
        [Route("Buy/PayPal")]
        public async Task<ActionResult> BuyTicket_PayPalAsync()
        {
            var orderId = Request.Query["token"];
            //this is where actual transaction is carried out
            HttpResponse response = await _payment.CaptureOrderAsync(orderId);
            Order result = response.Result<Order>();
            if (result.Status.Trim().ToUpper() == "COMPLETED")
            {
                _confirmationService.Confirm(Guid.Parse(HttpContext.User.Identity.Name));
                return Redirect(_configuration.GetSection("PayPal").GetValue<string>("successURL"));
            }
            else return Conflict();
        }

        [HttpGet]
        [Route("many/light")]
        [Authorize(AuthenticationSchemes = "Token")]
        public async Task<ActionResult<ConsertSelectorDto>> GetManyLightConcertsAsync(ConcertSelectParametersDto dto)
        {
            IQueryable<Models.Concert> concerts;
            if (dto.ByConcertType != null)
            {
                if (dto.ByConcertType == ConcertType.ClassicConcert) concerts = _context.ClassicConcerts;
                else if (dto.ByConcertType == ConcertType.OpenAirConcert) concerts = _context.OpenAirConcerts;
                else concerts = _context.PartyConcerts;
            }
            else concerts = _context.Concerts;
            if(dto.ByPerformer != null) concerts = concerts.Where(c => c.Performer == dto.ByPerformer);
            concerts = concerts.Where(c => c.Cost < dto.UntilPrice && c.Cost > dto.FromPrice);
            var concertsCount = concerts.Count();
            if (concertsCount > 0)
            {
                ConsertSelectorDto selector = new ConsertSelectorDto()
                {
                    PagesCount = concertsCount / dto.NeededCount,
                    CurrentPage = dto.NextPage,
                    Concerts = await concerts.Skip((dto.NextPage - 1) * dto.NeededCount).Take(dto.NeededCount).ToDtosAsync()
                };
                return selector;
            }
            else return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Token")]
        public async Task<ActionResult> AddConcertAsync(AddConcertDto dto)
        {
            Models.Concert concert;
            if (dto.ConcertType == ConcertType.ClassicConcert)
            {
                concert = dto.ToClassicConcert(Guid.Parse(HttpContext.User.Identity.Name));
                await _context.ClassicConcerts.AddAsync((Models.ClassicConcert)concert);
            }
            else if (dto.ConcertType == ConcertType.OpenAirConcert)
            {
                concert = dto.ToOpenAirConcert(Guid.Parse(HttpContext.User.Identity.Name));
                await _context.OpenAirConcerts.AddAsync((Models.OpenAirConcert)concert);
            }
            else
            {
                concert = dto.ToPartyConcert(Guid.Parse(HttpContext.User.Identity.Name));
                await _context.PartyConcerts.AddAsync((Models.PartyConcert)concert);
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetConcertAsync", new { concertId = concert.ConcertId });
        }
        [HttpPost]
        [Route("{concertId}/Activate")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Token")]
        public async Task<ActionResult> ActivateConcertAsync(int concertId)
        {
            var concert = await _context.Concerts.FirstOrDefaultAsync(c => concertId == c.ConcertId);
            if (concert != null && concert.IsActiveFlag == false)
            {
                concert.IsActiveFlag = true;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
        [HttpPost]
        [Route("{concertId}/Deactivate")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Token")]
        public async Task<ActionResult> DeactivateConcertAsync(int concertId)
        {
            var concert = await _context.Concerts.FirstOrDefaultAsync(c => concertId == c.ConcertId);
            if (concert != null && concert.IsActiveFlag == true)
            {
                concert.IsActiveFlag = false;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
    }
}
