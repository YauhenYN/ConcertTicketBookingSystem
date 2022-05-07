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
    public class MicrosoftOAuthService : OAuthServiceBase, IMicrosoftOAuthService
    {
        private readonly OAuth.Interfaces.IMicrosoftOAuthService _oAuthService;
        private readonly IMicrosoftUsersRepository _microsoftUsersRepository;
        private readonly IActionsRepository _actionsRepository;
        private readonly MicrosoftOAuthConf _microsoftOAuthConf;
        private readonly ISha256Helper _sha256Helper;
        public MicrosoftOAuthService(JwtServiceBase jwtService,
            OAuth.Interfaces.IMicrosoftOAuthService oAuthService,
            IActionsRepository actionsRepository,
            IMicrosoftUsersRepository microsoftUsersRepository,
            IOptions<MicrosoftOAuthConf> options,
            ISha256Helper sha256Helper)
            : base(jwtService)
        {
            _oAuthService = oAuthService;
            _actionsRepository = actionsRepository;
            _microsoftUsersRepository = microsoftUsersRepository;
            _microsoftOAuthConf = options.Value;
            _sha256Helper = sha256Helper;
        }

        public async Task<TokensResponse> CodeAsync(string code, string codeVerifier)
        {
            var tokenResult = await _oAuthService.ExchangeCodeOnTokenAsync(code, codeVerifier);
            var credentials = await _oAuthService.GetUserCredentialsAsync(tokenResult.AccessToken);
            string microsoftId = credentials.id;
            var user = await _microsoftUsersRepository.GetByIdAsync(microsoftId);
            if (user == null)
            {
                user = new MicrosoftUser()
                {
                    BirthDate = null,
                    CookieConfirmationFlag = false,
                    Email = credentials.mail != null ? credentials.mail : credentials.userPrincipalName,
                    MicrosoftId = credentials.id,
                    IsAdmin = false,
                    Name = credentials.displayName,
                    PromoCodeId = null,
                    UserId = Guid.NewGuid()
                };
                await _microsoftUsersRepository.AddAsync(user);
            }
            var response = GenerateAndRegisterTokensResponse(user);
            SetUsersRefreshToken(user, response.RefreshToken, response.RefreshExpirationTime);
            await _actionsRepository.AddActionAsync(user.UserId, "Logged In with Microsoft");
            await _actionsRepository.SaveChangesAsync();
            return response;
        }

        public Task<string> GenerateOAuthRequestUrlAsync(string codeVerifier)
        {
            var codeChellange = _sha256Helper.ComputeHash(codeVerifier);
            return Task.FromResult(_oAuthService.GenerateOAuthRequestUrl(_microsoftOAuthConf.Scope, codeChellange));
        }
    }
}
