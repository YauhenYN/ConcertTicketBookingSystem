using BLL.Dtos.AuthenticationDtos;
using System;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserAuthenticationService
    {
        public Task<TokensResponse> RefreshTokensAsync(Guid userId);
        public Task ClearRefreshTokenAsync(Guid userId);
    }
}
