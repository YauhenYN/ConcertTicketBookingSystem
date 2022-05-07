using BLL.Dtos.AuthenticationDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.Extensions.Options;
using BLL.Configurations;

namespace PL.Controllers
{
    [ApiController]
    [Route("Authentication/OAuth/Facebook")]
    public class FacebookOAuthController : OAuthBaseController
    {
        private readonly IFacebookOAuthService _oAuthService;
        private readonly FacebookOAuthConf _facebookOAuthConf;
        public FacebookOAuthController(IFacebookOAuthService facebookOAuthService,
            IOptions<FacebookOAuthConf> options)
        {
            _oAuthService = facebookOAuthService;
            _facebookOAuthConf = options.Value;
        }

        [HttpGet]
        [Route("Redirect")]
        public async Task<ActionResult> RedirectOnOAuthServerAsync()
        {
            var codeVerifier = Guid.NewGuid().ToString();
            HttpContext.Session.SetString(_codeVerifierName, codeVerifier);
            var url = await _oAuthService.GenerateOAuthRequestUrlAsync(codeVerifier);
            return Redirect(url);
        }
        [HttpGet]
        [Route("Code")]
        public async Task<ActionResult<TokensResponse>> CodeAsync(string code, string state)
        {
            if (code == null || state == null || HttpContext.Session.GetString(_codeVerifierName) != state) return BadRequest();
            var response = await _oAuthService.CodeAsync(code, state);
            AppendTokens(response.RefreshToken, response.RefreshExpirationTime);
            return RedirectPermanent(_facebookOAuthConf.RedirectUrl);
        }
    }
}
