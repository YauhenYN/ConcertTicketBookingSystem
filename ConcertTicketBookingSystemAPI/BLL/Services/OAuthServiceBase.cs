using DAL.Models;
using Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public abstract class OAuthServiceBase : AuthenticationServiceBase
    {
        public OAuthServiceBase(JwtServiceBase jwtService)
            :base(jwtService)
        {
        }
        protected void SetUsersRefreshToken(User user, string refreshToken, DateTime expirationTime)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = expirationTime;
        }
    }
}
