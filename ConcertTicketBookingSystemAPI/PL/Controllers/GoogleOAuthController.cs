using BLL.Dtos.AuthenticationDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Configurations;
using Microsoft.Extensions.Options;

namespace PL.Controllers
{
    [ApiController]
    [Route("Authentication/OAuth/Google")]
    public class GoogleOAuthController : OAuthBaseController
    {
        private readonly IGoogleOAuthService _oAuthService;
        private readonly GoogleOAuthConf _googleOAuthConf;

        public GoogleOAuthController(IGoogleOAuthService googleOAuthService,
            IOptions<GoogleOAuthConf> options)
        {
            _oAuthService = googleOAuthService;
            _googleOAuthConf = options.Value;
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
        public async Task<ActionResult<TokensResponse>> CodeAsync(string code, string scope)
        {
            if (code == null || scope == null) return BadRequest();
            string codeVerifier = HttpContext.Session.GetString(_codeVerifierName);
            var response = await _oAuthService.CodeAsync(code, codeVerifier);
            AppendTokens(response.RefreshToken, response.RefreshExpirationTime);
            return RedirectPermanent(_googleOAuthConf.RedirectUrl);
        }
    }
}
