using ConcertTicketBookingSystemAPI.CustomServices.OAuth;
using ConcertTicketBookingSystemAPI.Dtos.AuthenticationDtos;
using ConcertTicketBookingSystemAPI.Helpers;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("Authentication/OAuth/Microsoft")]
    public class MicrosoftOAuthController : ControllerBase
    {
        private readonly MicrosoftOAuthService _oAuthService;
        private readonly ApplicationContext _context;
        private readonly IConfigurationSection _facebookSection;
        public MicrosoftOAuthController(MicrosoftOAuthService oAuthService, IConfiguration configuration, ApplicationContext context)
        {
            _oAuthService = oAuthService;
            _context = context;
            _facebookSection = configuration.GetSection("MicrosoftOAuth");
        }

        [HttpGet]
        [Route("Redirect")]
        public ActionResult RedirectOnOAuthServer()
        {
            var scope = _facebookSection["scope"];
            var codeVerifier = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("codeVerifier", codeVerifier);
            var codeChellange = Sha256Helper.ComputeHash(codeVerifier);
            var url = _oAuthService.GenerateOAuthRequstUrl(scope, codeChellange);
            return Redirect(url);
        }
        [HttpGet]
        [Route("Code")]
        public async Task<ActionResult<TokensResponse>> CodeAsync(string code)
        {
            var request = HttpContext.Request;
            if (code == null) return BadRequest();
            string codeVerifier = HttpContext.Session.GetString("codeVerifier");
            var tokenResult = await _oAuthService.ExchangeCodeOnTokenAsync(code, codeVerifier);
            var credentials = await _oAuthService.GetUserCredentialsAsync(tokenResult.AccessToken);
            string microsoftId = credentials.id;
            var user = _context.MicrosoftUsers.FirstOrDefault(u => u.MicrosoftId == microsoftId);
            if (user == null)
            {
                user = new MicrosoftUser() { BirthDate = null, CookieConfirmationFlag = false, Email = credentials.mail != null ? credentials.mail : credentials.userPrincipalName, MicrosoftId = credentials.id, IsAdmin = false, Name = credentials.displayName, PromoCodeId = null, UserId = Guid.NewGuid() };
                await _context.MicrosoftUsers.AddAsync(user);
            }
            var response = JwtHelper.GenerateAndRegisterTokensResponse(user);
            user.RefreshToken = response.RefreshToken;
            user.RefreshTokenExpiryTime = response.RefreshExpirationTime;
            await _context.AddActionAsync(user.UserId, "Logged In with Microsoft");
            await _context.SaveChangesAsync();
            HttpContext.AppendTokens(response);
            return RedirectPermanent(_facebookSection["redirectUrl"]);
        }
    }
}
