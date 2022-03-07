using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.JwtAuth
{
    public class AuthOptions
    {
        public static string ISSUER { get; private set; }
        public static string AUDIENCE { get; private set; }
        private static string KEY;
        public static int LIFETIME { get; private set; }
        public static int REFRESHLIFETIME { get; private set; }
        public AuthOptions(IConfiguration configuration)
        {
            var section = configuration.GetSection("AuthOptions");
            ISSUER = section["Issuer"];
            AUDIENCE = section["Audience"];
            KEY = section["Key"];
            LIFETIME = section.GetValue<int>("LifeTime");
            REFRESHLIFETIME = section.GetValue<int>("RefreshLifeTime");
        }
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY)); /////////////////////////
        }
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToHexString(randomNumber);
            }
        }
        public static JwtSecurityToken GetToken(IEnumerable<Claim> claims) => new JwtSecurityToken(
          ISSUER,
          AUDIENCE,
          claims,
          notBefore: DateTime.Now,
          expires: DateTime.Now.Add(TimeSpan.FromMinutes(LIFETIME)),
          signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        public static ClaimsIdentity GetIsAdminIdentity<T>(T id, bool isAdmin)
        {
            var claims = new[]
            {
                   new Claim(ClaimsIdentity.DefaultNameClaimType, id.ToString()),
                   new Claim(ClaimsIdentity.DefaultRoleClaimType, isAdmin ? "admin" : "user")
            };
            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
