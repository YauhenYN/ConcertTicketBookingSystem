using BLL.Dtos.AuthenticationDtos;
using DAL.Models;
using Jwt;
using System;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BLL.Services
{
    public abstract class AuthenticationServiceBase
    {
        private readonly JwtServiceBase _jwtService;
        public AuthenticationServiceBase(JwtServiceBase jwtService)
        {
            _jwtService = jwtService;
        }
        private ClaimsIdentity GetIdentity<T>(T id, string role)
        {
            var claims = new[]
            {
                   new Claim(ClaimsIdentity.DefaultNameClaimType, id.ToString()),
                   new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToHexString(randomNumber);
            }
        }
        protected TokensResponse GenerateAndRegisterTokensResponse(User user)
        {
            var identity = GetIdentity(user.UserId, user.IsAdmin ? "admin" : "user");
            return new TokensResponse()
            {
                AccessToken = _jwtService.GenerateAndRegisterJwtToken(identity.Claims),
                ExpirationTime = DateTime.Now.AddMinutes(_jwtService.LIFETIME).ToUniversalTime(),
                RefreshToken = GenerateRefreshToken(),
                RefreshExpirationTime = DateTime.Now.AddMinutes(_jwtService.REFRESHLIFETIME).ToUniversalTime()
            };
        }
    }
}
