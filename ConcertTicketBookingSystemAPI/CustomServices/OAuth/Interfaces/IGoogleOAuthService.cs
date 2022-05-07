using System.Threading.Tasks;

namespace OAuth.Interfaces
{
    public interface IGoogleOAuthService : IOAuthService
    {
        public string GenerateOAuthRequestUrl(string scope, string codeChallenge);
        public Task<TokenResult> ExchangeCodeOnTokenAsync(string code, string codeVerifier);
    }
}
