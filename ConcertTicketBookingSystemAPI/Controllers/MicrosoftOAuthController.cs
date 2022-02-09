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

        [HttpGet]
        [Route("Redirect")]
        public ActionResult RedirectOnOAuthServer()
        {
            var scope = _configuration.GetSection("MicrosoftOAuth")["scope"];
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
