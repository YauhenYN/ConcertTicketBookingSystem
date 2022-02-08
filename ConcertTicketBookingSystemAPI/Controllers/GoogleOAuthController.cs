using ConcertTicketBookingSystemAPI.CustomServices;
using ConcertTicketBookingSystemAPI.Helpers;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class GoogleOAuthController : ControllerBase
    {
        private readonly GoogleOAuthService _oAuthService;
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;
        public GoogleOAuthController(GoogleOAuthService oAuthService, IConfiguration configuration, ApplicationContext context)
        {
            _oAuthService = oAuthService;
            _configuration = configuration;
            _context = context;
        }
        [HttpGet]
        [Route("Redirect")]
        public ActionResult RedirectOnOAuthServer()
        {
            var scope = _configuration.GetSection("GoogleOAuth")["scope"];
            var redirectUrl = _configuration["CurrentApiUrl"] + "/GoogleOAuth/Code";
            var codeVerifier = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("codeVerifier", codeVerifier);
            var codeChellange = Sha256Helper.ComputeHash(codeVerifier);
            var url = _oAuthService.GenerateOAuthRequstUrl(scope, redirectUrl, codeChellange);
            return Redirect(url);
        }
        [HttpGet]
        [Route("Code")]
        public async Task<ActionResult> CodeAsync(string code, string scope)
        {
            string codeVerifier = HttpContext.Session.GetString("codeVerifier");
            var redirectUrl = _configuration["CurrentApiUrl"] + "/GoogleOAuth/Code";
            var tokenResult = await _oAuthService.ExchangeCodeOnTokenAsync(code, codeVerifier, redirectUrl);
            var credentials = await _oAuthService.GetUserCredentialsAsync(tokenResult.AccessToken);
            string userGoogleId = credentials.id;
            var user = _context.GoogleUsers.FirstOrDefault(u => u.GoogleId == userGoogleId);
            if (user == null)
            {
                user = new GoogleUser() { BirthDate = null, CookieConfirmationFlag = false, Email = credentials.email, GoogleId = credentials.id, IsAdmin = false, Name = credentials.name, PromoCodeId = null, UserId = Guid.NewGuid() };
                await _context.GoogleUsers.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            var identity = GetIdentity(user.UserId, user.IsAdmin);
            var token = new JwtSecurityToken(
                JwtAuth.AuthOptions.ISSUER,
                JwtAuth.AuthOptions.AUDIENCE,
                identity.Claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(JwtAuth.AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(JwtAuth.AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var response = new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                username = identity.Name
            };
            return new JsonResult(response);
        }
        private ClaimsIdentity GetIdentity(Guid id, bool isAdmin)
        {
            var claims = new[]
{
                   new Claim(ClaimsIdentity.DefaultNameClaimType, id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, isAdmin ? "admin" : "user")
            };
            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
