using ConcertTicketBookingSystemAPI.CustomServices;
using ConcertTicketBookingSystemAPI.CustomServices.OAuth;
using ConcertTicketBookingSystemAPI.Dtos.AuthenticationDtos;
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
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("Authentication/OAuth/Microsoft")]
    public class MicrosoftOAuthController : ControllerBase
    {
        private readonly MicrosoftOAuthService _oAuthService;
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;
        public MicrosoftOAuthController(MicrosoftOAuthService oAuthService, IConfiguration configuration, ApplicationContext context)
        {
            _oAuthService = oAuthService;
            _configuration = configuration;
            _context = context;
        }
        private string RedirectURL() => _configuration.GetSection("GoogleOAuth")["OAuthRedirect"];

        [HttpGet]
        [Route("Redirect")]
        public ActionResult RedirectOnOAuthServer()
        {
            var scope = _configuration.GetSection("GoogleOAuth")["scope"];
            var redirectUrl = RedirectURL();
            var codeVerifier = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("codeVerifier", codeVerifier);
            var codeChellange = Sha256Helper.ComputeHash(codeVerifier);
            var url = _oAuthService.GenerateOAuthRequstUrl(scope, redirectUrl, codeChellange);
            return Redirect(url);
        }
        [HttpGet]
        [Route("Code")]
        public async Task<ActionResult<TokensResponse>> CodeAsync(string code, string scope)
        {
            if (code == null || scope == null) return BadRequest();
            string codeVerifier = HttpContext.Session.GetString("codeVerifier");
            var redirectUrl = RedirectURL();
            var tokenResult = await _oAuthService.ExchangeCodeOnTokenAsync(code, codeVerifier, redirectUrl);
            //var refreshTokenResult = await _oAuthService.RefreshTokenAsync(tokenResult.RefreshToken);
            var credentials = await _oAuthService.GetUserCredentialsAsync(tokenResult.AccessToken);
            string userGoogleId = credentials.id;
            var user = _context.GoogleUsers.FirstOrDefault(u => u.GoogleId == userGoogleId);
            if (user == null)
            {
                user = new GoogleUser() { BirthDate = null, CookieConfirmationFlag = false, Email = credentials.email, GoogleId = credentials.id, IsAdmin = false, Name = credentials.name, PromoCodeId = null, UserId = Guid.NewGuid() };
                await _context.GoogleUsers.AddAsync(user);
            }
            var response = GenerateAndRegisterTokensResponse(user);
            user.RefreshToken = response.RefreshToken;
            user.RefreshTokenExpiryTime = response.ExpirationTime;
            await _context.SaveChangesAsync();
            return response;
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
