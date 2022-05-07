using System;
using System.Security.Claims;

namespace UnitTests
{
    public static class Helper
    {
        public static ClaimsPrincipal GetClaimsPrincipal(Guid userId, string role)
        {
            var claims = new[]
            {
                   new Claim(ClaimsIdentity.DefaultNameClaimType, userId.ToString()),
                   new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin")
            };
            return new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType));
        }
    }
}
