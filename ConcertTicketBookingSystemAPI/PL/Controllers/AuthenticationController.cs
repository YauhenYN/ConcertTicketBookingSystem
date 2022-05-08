using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : AuthenticationControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IUserInfoService _userInfoService;
        public AuthenticationController(IUserAuthenticationService userAuthenticationService,
            IUserInfoService userInfoService)
        {
            _userAuthenticationService = userAuthenticationService;
            _userInfoService = userInfoService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<string>> RefreshAsync()
        {
            var user = await _userInfoService.GetUserInfoByRefreshTokenAsync(HttpContext.Request.Cookies["RefreshToken"]);
            if (user != null)
            {
                var response = await _userAuthenticationService.RefreshTokensAsync(user.UserId);
                AppendTokens(response.RefreshToken, response.RefreshExpirationTime);
                return response.AccessToken;
            }
            else return Conflict();
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> LogOutAsync()
        {
            await _userAuthenticationService.ClearRefreshTokenAsync(UserId.Value);
            Response.Cookies.Delete("RefreshToken");
            return Ok();
        }
    }
}
