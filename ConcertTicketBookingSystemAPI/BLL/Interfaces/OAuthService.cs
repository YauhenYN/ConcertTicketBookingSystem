using BLL.Dtos.AuthenticationDtos;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface OAuthService
    {
        public Task<string> GenerateOAuthRequestUrlAsync(string codeVerifier);
        public Task<TokensResponse> CodeAsync(string code, string codeVerifier);
    }
}
