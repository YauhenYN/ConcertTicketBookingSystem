using BLL.Configurations;
using BLL.Dtos.PersonalizationDtos;
using BLL.Interfaces;
using ConfirmationService;
using DAL.Interfaces;
using EmailSender;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PersonalizationService : IPersonalizationService
    {
        private readonly IConfirmationService<Guid, IUsersRepository> _confirmationService;
        private readonly IUsersRepository _usersRepository;
        private readonly IPromoCodesRepository _promoCodesRepository;
        private readonly IActionsRepository _actionsRepository;
        private readonly IEmailSending _emailSenderService;
        private readonly BaseLinksConf _baseLinksConf;
        private const int maxSendAttemptsCount = 5;

        public PersonalizationService(IConfirmationService<Guid, IUsersRepository> confirmationService,
            IUsersRepository usersRepository,
            IPromoCodesRepository promoCodesRepository,
            IActionsRepository actionsRepository,
            IEmailSending emailSenderService,
            IOptions<BaseLinksConf> options)
        {
            _emailSenderService = emailSenderService;
            _usersRepository = usersRepository;
            _promoCodesRepository = promoCodesRepository;
            _actionsRepository = actionsRepository;
            _confirmationService = confirmationService;
            _baseLinksConf = options.Value;
        }

        public async Task ActivatePromoCodeAsync(ActivatePromocodeDto dto, Guid userId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            var promoCode = await _promoCodesRepository.GetByCodeAsync(dto.Code);
            promoCode.LeftCount--;
            user.PromoCodeId = promoCode.PromoCodeId;
            await _usersRepository.SaveChangesAsync();
        }

        public async Task ConfirmCookiesAsync(Guid userId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            user.CookieConfirmationFlag = true;
            await _usersRepository.SaveChangesAsync();
        }

        public async Task RemoveRightsAsync(Guid userId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            user.IsAdmin = false;
            await _usersRepository.SaveChangesAsync();
        }

        public async Task UpdateBirthYearAsync(UpdateBirthYearDto dto, Guid userId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            user.BirthDate = dto.BirthYear;
            await _actionsRepository.AddActionAsync(userId, "Updated BirthYear");
            await _actionsRepository.SaveChangesAsync();
        }

        public async Task UpdateEmailAsync(UpdateEmailDto dto, Guid userId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            var secretGuid = Guid.NewGuid();
            _emailSenderService.SendHtml("EmailConfirmation", dto.NewEmail,
                "<a href=\"" + _baseLinksConf.CurrentApiUrl + "/EmailConfirmation/Confirm?confirmationCode=" + 
                secretGuid + "\">Подтвердить новый Email</a>", maxSendAttemptsCount);
            _confirmationService.Add(secretGuid, async (usersRepository) =>
            {
                user = await usersRepository.GetByIdAsync(user.UserId);
                user.Email = dto.NewEmail;
                await usersRepository.SaveChangesAsync();
            });
        }

        public async Task UpdateNameAsync(UpdateNameDto dto, Guid userId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            user.Name = dto.NewName;
            await _actionsRepository.AddActionAsync(userId, "Updated Name");
            await _actionsRepository.SaveChangesAsync();
        }
    }
}
