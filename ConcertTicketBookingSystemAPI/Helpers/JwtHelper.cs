using ConcertTicketBookingSystemAPI.Dtos.AuthenticationDtos;
using ConcertTicketBookingSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Helpers
{
    public static class JwtHelper
    {
        public static TokensResponse GenerateAndRegisterTokensResponse(User user)
        {
            var identity = JwtAuth.AuthOptions.GetIsAdminIdentity(user.UserId, user.IsAdmin);
            var token = JwtAuth.AuthOptions.GetToken(identity.Claims);
            return new TokensResponse()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token), //
                ExpirationTime = DateTime.Now.AddMinutes(JwtAuth.AuthOptions.LIFETIME).ToUniversalTime(),
                RefreshToken = JwtAuth.AuthOptions.GenerateRefreshToken(),
                RefreshExpirationTime = DateTime.Now.AddMinutes(JwtAuth.AuthOptions.REFRESHLIFETIME).ToUniversalTime()
            };
        }
    }
}
