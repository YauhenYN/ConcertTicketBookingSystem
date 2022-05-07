using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jwt
{
    public class JwtService : JwtServiceBase
    {
        public string ISSUER { get; private set; }
        public string AUDIENCE { get; private set; }
        private string KEY;

        public JwtService(string issuer, string audience, string key, int lifeTime, int refreshLifeTime)
        {
            ISSUER = issuer;
            AUDIENCE = audience;
            KEY = key;
            LIFETIME = lifeTime;
            REFRESHLIFETIME = refreshLifeTime;
        }
        public override string GenerateAndRegisterJwtToken(IEnumerable<Claim> claims)
        {
            var token = GetToken(claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
        private JwtSecurityToken GetToken(IEnumerable<Claim> claims) =>
            new JwtSecurityToken(ISSUER, AUDIENCE, claims, notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(LIFETIME), signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), 
                    SecurityAlgorithms.HmacSha256));
    }
}
