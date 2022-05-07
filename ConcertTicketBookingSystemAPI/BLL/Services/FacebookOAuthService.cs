using BLL.Dtos.AuthenticationDtos;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using Jwt;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FacebookOAuthService : OAuthServiceBase, IFacebookOAuthService
    {
        private readonly OAuth.Interfaces.IFacebookOAuthService _oAuthService;
        private readonly IFacebookUsersRepository _facebookUsersRepository;
        private readonly IActionsRepository _actionsRepository;
        public FacebookOAuthService(JwtServiceBase jwtService, 
            OAuth.Interfaces.IFacebookOAuthService oAuthService,
            IFacebookUsersRepository facebookUsersRepository,
            IActionsRepository actionsRepository)
            :base(jwtService)
        {
            _oAuthService = oAuthService;
            _facebookUsersRepository = facebookUsersRepository;
            _actionsRepository = actionsRepository;
        }

        public async Task<TokensResponse> CodeAsync(string code, string state)
        {
            var tokenResult = await _oAuthService.ExchangeCodeOnTokenAsync(code);
            var credentials = await _oAuthService.GetUserCredentialsAsync(tokenResult.AccessToken);
            string userFacebookId = credentials.id;
            var user = await _facebookUsersRepository.GetByIdAsync(userFacebookId);
            if (user == null)
            {
                user = new FacebookUser()
                {
                    BirthDate = null,
                    CookieConfirmationFlag = false,
                    Email = credentials.email,
                    FacebookId = credentials.id,
                    IsAdmin = false,
                    Name = credentials.name,
                    PromoCodeId = null,
                    UserId = Guid.NewGuid()
                };
                await _facebookUsersRepository.AddAsync(user);
            }
            var response = GenerateAndRegisterTokensResponse(user);
            SetUsersRefreshToken(user, response.RefreshToken, response.RefreshExpirationTime);
            await _actionsRepository.AddActionAsync(user.UserId, "Logged In with Facebook");
            await _actionsRepository.SaveChangesAsync();
            return response;
        }

        public Task<string> GenerateOAuthRequestUrlAsync(string codeVerifier)
        {
            return Task.FromResult(_oAuthService.GenerateOAuthRequestUrl(codeVerifier));
        }
    }
}
