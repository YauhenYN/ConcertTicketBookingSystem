using ConcertTicketBookingSystemAPI.Dtos.AuthenticationDtos;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
                var response = GenerateAndRegisterTokensResponse(user);
                user.RefreshToken = response.RefreshToken;
                user.RefreshTokenExpiryTime = response.ExpirationTime;
                await _context.SaveChangesAsync();
                return response;
            }
            else return Conflict();
        }
        private TokensResponse GenerateAndRegisterTokensResponse(User user)
        {
            var identity = JwtAuth.AuthOptions.GetIsAdminIdentity(user.UserId, user.IsAdmin);
            var token = JwtAuth.AuthOptions.GetToken(identity.Claims);
            return new TokensResponse()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token), //
                RefreshToken = JwtAuth.AuthOptions.GenerateRefreshToken(),
                ExpirationTime = DateTime.Now.AddMinutes(JwtAuth.AuthOptions.REFRESHLIFETIME).ToUniversalTime()
        };
        }
    }
}
