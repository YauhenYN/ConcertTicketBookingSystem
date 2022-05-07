using System.Collections.Generic;
using System.Security.Claims;

namespace Jwt
{
    public abstract class JwtServiceBase
    {
        public int LIFETIME { get; protected set; }
        public int REFRESHLIFETIME { get; protected set; }
        public abstract string GenerateAndRegisterJwtToken(IEnumerable<Claim> claims);
    }
}
