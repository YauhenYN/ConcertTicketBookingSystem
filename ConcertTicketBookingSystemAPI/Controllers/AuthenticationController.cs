using ConcertTicketBookingSystemAPI.Dtos.AuthenticationDtos;
using ConcertTicketBookingSystemAPI.Helpers;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<string>> RefreshAsync()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshTokenExpiryTime > DateTime.UtcNow && u.RefreshToken == HttpContext.Request.Cookies["RefreshToken"]);
            if (user != null)
            {
                var response = JwtHelper.GenerateAndRegisterTokensResponse(user);
                user.RefreshToken = response.RefreshToken;
                user.RefreshTokenExpiryTime = response.RefreshExpirationTime;
                await _context.SaveChangesAsync();
                HttpContext.AppendTokens(response);
                return response.AccessToken;
            }
            else return Conflict();
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> LogOutAsync()
        {
            var user = (await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(HttpContext.User.Identity.Name)));
            user.RefreshToken = "";
            await _context.AddActionAsync(Guid.Parse(HttpContext.User.Identity.Name), "Log out");
            await _context.SaveChangesAsync();
            Response.Cookies.Delete("RefreshToken");
            return Ok();
        }
    }
}
