using ConcertTicketBookingSystemAPI.CustomServices;
using ConcertTicketBookingSystemAPI.CustomServices.OAuth;
using ConcertTicketBookingSystemAPI.Dtos.AuthenticationDtos;
using ConcertTicketBookingSystemAPI.Helpers;
using ConcertTicketBookingSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("Authentication/OAuth/Google")]
    public class GoogleOAuthController : ControllerBase
    {
        private readonly GoogleOAuthService _oAuthService;
        private readonly ApplicationContext _context;
        private readonly IConfigurationSection _googleSection;
        public GoogleOAuthController(GoogleOAuthService oAuthService, IConfiguration configuration, ApplicationContext context)
        {
            _oAuthService = oAuthService;
            _context = context;
            _googleSection = configuration.GetSection("GoogleOAuth");
        }

        [HttpGet]
        [Route("Redirect")]
        public ActionResult RedirectOnOAuthServer()
        {
            var scope = _googleSection["scope"];
 
            var codeVerifier = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("codeVerifier", codeVerifier);
            var codeChellange = Sha256Helper.ComputeHash(codeVerifier);
            var url = _oAuthService.GenerateOAuthRequstUrl(scope, codeChellange);
            return Redirect(url);
        }
        [HttpGet]
        [Route("Code")]
        public async Task<ActionResult<TokensResponse>> CodeAsync(string code, string scope)
        {
            if (code == null || scope == null) return BadRequest();
            string codeVerifier = HttpContext.Session.GetString("codeVerifier");
            HttpContext.Session.Remove("codeVerifier");
            var tokenResult = await _oAuthService.ExchangeCodeOnTokenAsync(code, codeVerifier);
            //var refreshTokenResult = await _oAuthService.RefreshTokenAsync(tokenResult.RefreshToken);
            var credentials = await _oAuthService.GetUserCredentialsAsync(tokenResult.AccessToken);
            string userGoogleId = credentials.id;
            var user = _context.GoogleUsers.FirstOrDefault(u => u.GoogleId == userGoogleId);
            if (user == null)
            {
                user = new GoogleUser() { BirthDate = null, CookieConfirmationFlag = false, Email = credentials.email, GoogleId = credentials.id, IsAdmin = false, Name = credentials.name, PromoCodeId = null, UserId = Guid.NewGuid() };
                await _context.GoogleUsers.AddAsync(user);
            }
            var response = JwtHelper.GenerateAndRegisterTokensResponse(user);
            user.RefreshToken = response.RefreshToken;
            user.RefreshTokenExpiryTime = response.RefreshExpirationTime;
            await _context.SaveChangesAsync();
            HttpContext.AppendTokens(response);
            return RedirectPermanent(_googleSection["redirectUrl"]);
        }
    }
}
