using ConcertTicketBookingSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
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
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AdministrationController> _logger;
        ApplicationContext _context;

        public AuthenticationController(ILogger<AdministrationController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        private string GetToken(Guid id, bool isAdmin)
        {
            var claims = new[]
            {
                   new Claim(ClaimsIdentity.DefaultNameClaimType, id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, isAdmin ? "admin" : "user")
            };
            var token = new JwtSecurityToken(
                JwtAuth.AuthOptions.ISSUER,
                JwtAuth.AuthOptions.AUDIENCE,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(JwtAuth.AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(JwtAuth.AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
