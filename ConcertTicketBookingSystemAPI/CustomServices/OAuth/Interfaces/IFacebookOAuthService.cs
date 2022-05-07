using System.Threading.Tasks;

namespace OAuth.Interfaces
{
    public interface IFacebookOAuthService : IOAuthService
    {
        public string GenerateOAuthRequestUrl(string state);
        public Task<TokenResult> ExchangeCodeOnTokenAsync(string code);
    }
}
