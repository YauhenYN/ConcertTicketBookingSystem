using Microsoft.AspNetCore.Http;
using System;

namespace PL.Controllers
{
    public abstract class AuthenticationControllerBase : CustomControllerBase
    {
        protected void AppendTokens(string refreshToken, DateTime refreshExpirationTime)
        {
            HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions()
            {
                Expires = refreshExpirationTime,
                HttpOnly = true
            });
        }
    }
}
