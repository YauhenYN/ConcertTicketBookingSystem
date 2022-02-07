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
            var scope = "https://www.googleapis.com/auth/userinfo.email";
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
            (string id, string email) credentials = await _oAuthService.GetUserCredentialsAsync(tokenResult.AccessToken);
            var user = _context.GoogleUsers.FirstOrDefault(u => u.GoogleId == int.Parse(credentials.id));
            if(user == null)
            {
                user = new GoogleUser() { BirthDate = null, CookieConfirmationFlag = false, Email = credentials.email, GoogleId = int.Parse(credentials.id), IsAdmin = false, Name = credentials.email, PromoCodeId = null, UserId = Guid.NewGuid() };
                await _context.GoogleUsers.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            //Authenticate
            return Ok();
        }
        private ClaimsIdentity GetIdentity(int id, bool isAdmin)
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
