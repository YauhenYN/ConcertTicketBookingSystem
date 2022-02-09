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
    [Route("Authentication/OAuth/Facebook")]
    public class FacebookOAuthController : ControllerBase
    {
        private readonly FacebookOAuthService _oAuthService;
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;
        public FacebookOAuthController(FacebookOAuthService oAuthService, IConfiguration configuration, ApplicationContext context)
        {
            _oAuthService = oAuthService;
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        [Route("Redirect")]
        public ActionResult RedirectOnOAuthServer()
        {
            var codeVerifier = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("codeVerifier", codeVerifier);
            var url = _oAuthService.GenerateOAuthRequstUrl(codeVerifier);
            return Redirect(url);
        }
        [HttpGet]
        [Route("Code")]
        public async Task<ActionResult<TokensResponse>> CodeAsync(string code, string state)
        {
            if (code == null || state == null && HttpContext.Session.GetString("codeVerifier") == state) return BadRequest(); 
            var tokenResult = await _oAuthService.ExchangeCodeOnTokenAsync(code);
            var credentials = await _oAuthService.GetUserCredentialsAsync(tokenResult.AccessToken);
            string userFacebookId = credentials.id;
            var user = _context.FacebookUsers.FirstOrDefault(u => u.FacebookId == userFacebookId);
            if (user == null)
            {
                user = new FacebookUser() { BirthDate = null, CookieConfirmationFlag = false, Email = credentials.email, FacebookId = credentials.id, IsAdmin = false, Name = credentials.name, PromoCodeId = null, UserId = Guid.NewGuid() };
                await _context.FacebookUsers.AddAsync(user);
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
