using BLL.Dtos.AuthenticationDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Configurations;
using Microsoft.Extensions.Options;

namespace PL.Controllers
{
    [ApiController]
    [Route("Authentication/OAuth/Microsoft")]
    public class MicrosoftOAuthController : OAuthBaseController
    {
        private readonly IMicrosoftOAuthService _oAuthService;
        private readonly MicrosoftOAuthConf _microsoftOAuthConf;
        public MicrosoftOAuthController(IMicrosoftOAuthService microsoftOAuthService,
            IOptions<MicrosoftOAuthConf> options)
        {
            _oAuthService = microsoftOAuthService;
            _microsoftOAuthConf = options.Value;
        }

        [HttpGet]
        [Route("Redirect")]
        public async Task<ActionResult> RedirectOnOAuthServerAsync()
        {
            var codeVerifier = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("codeVerifier", codeVerifier);
            var url = await _oAuthService.GenerateOAuthRequestUrlAsync(codeVerifier);
            return Redirect(url);
        }
        [HttpGet]
        [Route("Code")]
        public async Task<ActionResult<TokensResponse>> CodeAsync(string code)
        {
            if (code == null) return BadRequest();
            string codeVerifier = HttpContext.Session.GetString("codeVerifier");
            var response = await _oAuthService.CodeAsync(code, codeVerifier);
            AppendTokens(response.RefreshToken, response.RefreshExpirationTime);
            return RedirectPermanent(_microsoftOAuthConf.RedirectUrl);
        }
    }
}
