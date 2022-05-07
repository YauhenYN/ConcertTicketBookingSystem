using System.Threading.Tasks;

namespace OAuth.Interfaces
{
    public interface IMicrosoftOAuthService : IOAuthService
    {
        public string GenerateOAuthRequestUrl(string scope, string codeChallenge);
        public Task<TokenResult> ExchangeCodeOnTokenAsync(string code, string codeVerifier);
    }
}
