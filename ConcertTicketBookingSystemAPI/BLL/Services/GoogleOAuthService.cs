using BLL.Configurations;
using BLL.Dtos.AuthenticationDtos;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using Jwt;
using Microsoft.Extensions.Options;
using Sha256Helper;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GoogleOAuthService : OAuthServiceBase, IGoogleOAuthService
    {
        private readonly OAuth.Interfaces.IGoogleOAuthService _oAuthService;
        private readonly IGoogleUsersRepository _googleUsersRepository;
        private readonly IActionsRepository _actionsRepository;
        private readonly GoogleOAuthConf _googleOAuthConf;
        private readonly ISha256Helper _sha256Helper;
        public GoogleOAuthService(JwtServiceBase jwtService,
            OAuth.Interfaces.IGoogleOAuthService oAuthService,
            IGoogleUsersRepository googleUsersRepository,
            IActionsRepository actionsRepository,
            IOptions<GoogleOAuthConf> options,
            ISha256Helper sha256Helper)
            : base(jwtService)
        {
            _oAuthService = oAuthService;
            _googleUsersRepository = googleUsersRepository;
            _actionsRepository = actionsRepository;
            _googleOAuthConf = options.Value;
            _sha256Helper = sha256Helper;
        }

        public async Task<TokensResponse> CodeAsync(string code, string codeVerifier)
        {
            var tokenResult = await _oAuthService.ExchangeCodeOnTokenAsync(code, codeVerifier);
            var credentials = await _oAuthService.GetUserCredentialsAsync(tokenResult.AccessToken);
            string userGoogleId = credentials.id;
            var user = await _googleUsersRepository.GetByIdAsync(userGoogleId);
            if (user == null)
            {
                user = new GoogleUser()
                {
                    BirthDate = null,
                    CookieConfirmationFlag = false,
                    Email = credentials.email,
                    GoogleId = credentials.id,
                    IsAdmin = false,
                    Name = credentials.name,
                    PromoCodeId = null,
                    UserId = Guid.NewGuid()
                };
                await _googleUsersRepository.AddAsync(user);
            }
            var response = GenerateAndRegisterTokensResponse(user);
            SetUsersRefreshToken(user, response.RefreshToken, response.RefreshExpirationTime);
            await _actionsRepository.AddActionAsync(user.UserId, "Logged In with Google");
            await _actionsRepository.SaveChangesAsync();
            return response;
        }

        public Task<string> GenerateOAuthRequestUrlAsync(string codeVerifier)
        {
            var codeChellange = _sha256Helper.ComputeHash(codeVerifier);
            return Task.FromResult(_oAuthService.GenerateOAuthRequestUrl(_googleOAuthConf.Scope, codeChellange));
        }
    }
}
