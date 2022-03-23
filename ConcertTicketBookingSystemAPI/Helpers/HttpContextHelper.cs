using ConcertTicketBookingSystemAPI.Dtos.AuthenticationDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Helpers
{
    public static class HttpContextHelper
    {
        public static void AppendTokens(this HttpContext context, TokensResponse response)
        {
            context.Response.Cookies.Append("RefreshToken", response.RefreshToken, new CookieOptions()
            {
                Expires = response.RefreshExpirationTime,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });
        }
    }
}
