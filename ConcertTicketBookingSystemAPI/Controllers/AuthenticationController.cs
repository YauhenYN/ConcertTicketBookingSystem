using ConcertTicketBookingSystemAPI.Dtos.AuthenticationDtos;
using ConcertTicketBookingSystemAPI.Helpers;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AdministrationController> _logger;
        ApplicationContext _context;

        public AuthenticationController(ILogger<AdministrationController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = "Token")]
        public async Task<ActionResult<TokensResponse>> RefreshAsync(string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(HttpContext.User.Identity.Name));
            if (user.RefreshToken != null && refreshToken == user.RefreshToken && DateTime.Now.ToUniversalTime() < user.RefreshTokenExpiryTime)
            {
                var response = OAuthHelper.GenerateAndRegisterTokensResponse(user);
                user.RefreshToken = response.RefreshToken;
                user.RefreshTokenExpiryTime = response.RefreshExpirationTime;
                await _context.SaveChangesAsync();
                return response;
            }
            else return Conflict();
        }
    }
}
