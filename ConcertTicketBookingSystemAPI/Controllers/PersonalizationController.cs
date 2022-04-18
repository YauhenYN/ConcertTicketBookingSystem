using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTicketBookingSystemAPI.Dtos.PersonalizationDtos;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ConcertTicketBookingSystemAPI.CustomServices;
using Microsoft.Extensions.Configuration;
using ConcertTicketBookingSystemAPI.CustomServices.ConfirmationService;
using ConcertTicketBookingSystemAPI.CustomServices.EmailSending;
using ConcertTicketBookingSystemAPI.Helpers;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PersonalizationController : ControllerBase
    {
        private readonly ILogger<PersonalizationController> _logger;
        private readonly ApplicationContext _context;
        private readonly IConfirmationService<Guid, DbContext> _confirmationService;
        private readonly EmailSenderService _emailSenderService;
        private readonly IConfiguration _configuration;

        public PersonalizationController(ILogger<PersonalizationController> logger, ApplicationContext context, 
            IConfirmationService<Guid, DbContext> confirmationService, EmailSenderService emailSenderService, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _emailSenderService = emailSenderService;
            _confirmationService = confirmationService;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> UpdateBirthYearAsync(UpdateBirthYearDto dto)
        {
            var user = await CurrentUserAsync();
            user.BirthDate = dto.BirthYear;
            await _context.AddActionAsync(Guid.Parse(HttpContext.User.Identity.Name), "Updated BirthYear");
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> UpdateNameAsync(UpdateNameDto dto)
        {
            var user = await CurrentUserAsync();
            user.Name = dto.NewName;
            await _context.AddActionAsync(Guid.Parse(HttpContext.User.Identity.Name), "Updated Name");
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> UpdateEmailAsync(UpdateEmailDto dto)
        {
            var user = await CurrentUserAsync();
            var secretGuid = Guid.NewGuid();
            _emailSenderService.SendHtml("EmailConfirmation", dto.NewEmail, 
                "<a href=\"" + _configuration["CurrentApiUrl"] + "/EmailConfirmation/Confirm?confirmationCode=" + secretGuid +"\">Подтвердить новый Email</a>", 5);
            _confirmationService.Add(secretGuid, (context) =>
            {
                user = ((ApplicationContext)context).Users.FirstOrDefault(u => u.UserId == user.UserId);
                user.Email = dto.NewEmail;
                ((ApplicationContext)context).AddAction(user.UserId, "Updated Email");
                ((ApplicationContext)context).SaveChanges();
            });
            return NoContent();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RemoveRightsAsync()
        {
            var user = await CurrentUserAsync();
            user.IsAdmin = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> ActivatePromocodeAsync(ActivatePromocodeDto dto)
        {
            var user = await CurrentUserAsync();
            var promoCode = await _context.PromoCodes.FirstOrDefaultAsync(p => p.Code == dto.Code);
            if(promoCode != null && (user.PromoCode == null || promoCode.Discount > user.PromoCode.Discount))
            {
                promoCode.LeftCount--;
                user.PromoCodeId = promoCode.PromoCodeId;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> ConfirmCookiesAsync()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(HttpContext.User.Identity.Name));
            if (user.CookieConfirmationFlag == true) return Conflict();
            else
            {
                user.CookieConfirmationFlag = true;
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<UserInfoDto>> UserInfoAsync()
        {
            return (await _context.Users.FirstAsync(u => u.UserId == Guid.Parse(HttpContext.User.Identity.Name))).ToUserInfoDto();
        }
        private async Task<User> CurrentUserAsync() => await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(HttpContext.User.Identity.Name));
    }
}
