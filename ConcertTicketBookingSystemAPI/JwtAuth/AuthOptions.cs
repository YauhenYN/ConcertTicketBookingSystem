using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public AuthOptions(IConfiguration configuration)
        {
            var section = configuration.GetSection("AuthOptions");
            ISSUER = section["Issuer"];
            AUDIENCE = section["Audience"];
            KEY = section["Key"];
            LIFETIME = section.GetValue<int>("LifeTime");
        }
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
