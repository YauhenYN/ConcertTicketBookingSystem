using BLL.Dtos.AuthenticationDtos;
using BLL.Interfaces;
using Jwt;
using System;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace BLL.Services
{
    public class UserAuthenticationService : AuthenticationServiceBase, IUserAuthenticationService
    {
        private readonly IActionsRepository _actionsRepository;
        private readonly IUsersRepository _usersRepository;

        public UserAuthenticationService(JwtServiceBase jwtService,
            IActionsRepository actionsRepository,
            IUsersRepository usersRepository)
            :base(jwtService)
        {
            _actionsRepository = actionsRepository;
            _usersRepository = usersRepository;
        }

        public async Task ClearRefreshTokenAsync(Guid userId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            user.RefreshToken = "";
            await _actionsRepository.AddActionAsync(userId, "Logged out");
            await _actionsRepository.SaveChangesAsync();
        }

        public async Task<TokensResponse> RefreshTokensAsync(Guid userId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            var response = GenerateAndRegisterTokensResponse(user);
            user.RefreshToken = response.RefreshToken;
            user.RefreshTokenExpiryTime = response.RefreshExpirationTime;
            await _usersRepository.SaveChangesAsync();
            return response;
        }
    }
}
