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

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PersonalizationController : ControllerBase
    {
        private readonly ILogger<PersonalizationController> _logger;
        private readonly ApplicationContext _context;
        private readonly IConfirmationService<Guid> _confirmationService;
        private readonly EmailSenderService _emailSenderService;
        private readonly IConfiguration _configuration;

        public PersonalizationController(ILogger<PersonalizationController> logger, ApplicationContext context, IConfirmationService<Guid> confirmationService, EmailSenderService emailSenderService, IConfiguration configuration)
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
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> UpdateNameAsync(UpdateNameDto dto)
        {
            var user = await CurrentUserAsync();
            user.Name = dto.NewName;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> UpdateEmailAsync(UpdateEmailDto dto)
        {
            var user = await CurrentUserAsync();
            var secretGuid = Guid.NewGuid();
            await _emailSenderService.SendHtmlAsync("EmailConfirmation", dto.NewEmail, "<a href=\"" + _configuration["CurrentApiUrl"] + "\\EmailConfirmation\\Confirm" + secretGuid +"\">Подтвердить новый Email</a>");
            _confirmationService.Add(secretGuid, async () =>
            {
                user.Email = dto.NewEmail;
                await _context.SaveChangesAsync();
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
            if(promoCode != null && promoCode.Discount > user.PromoCode.Discount)
            {
                user.PromoCodeId = promoCode.PromoCodeId;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> BuyTicket_PayPalAsync(BuyTicketDto dto)
        {
            return Ok();
        }
        private async Task<User> CurrentUserAsync() => await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(HttpContext.User.Identity.Name));
    }
}
