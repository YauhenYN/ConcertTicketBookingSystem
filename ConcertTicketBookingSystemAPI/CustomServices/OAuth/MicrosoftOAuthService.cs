using HttpClientHelper;
using OAuth.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OAuth
{
    public class MicrosoftOAuthService : OAuthServiceBase, IMicrosoftOAuthService
    {
        private readonly string _tenant;
        protected readonly string _refreshEndPoint;
        public MicrosoftOAuthService(HttpClientHelperBase httpClientHelper,
            string tenant,
            string clientId,
            string secret,
            string oAuthServerEndPoint,
            string tokenEndPoint,
            string refreshEndPoint,
            string redirectUrl)
            : base(httpClientHelper, clientId, secret, oAuthServerEndPoint, tokenEndPoint, redirectUrl, "https://graph.microsoft.com/v1.0/me")
        {
            _tenant = tenant;
            _refreshEndPoint = refreshEndPoint;
        }
        public string GenerateOAuthRequestUrl(string scope, string codeChallenge)
        {
            var queryParams = new Dictionary<string, string>()
            {
                { "tenant", _tenant},
                { "client_id", _clientId},
                { "response_type", "code"},
                { "redirect_uri", _redirectUrl},
                { "scope", scope},
                { "response_mode", "query"},
                { "code_challenge", codeChallenge},
                { "code_challenge_method", "S256" }
            };
            var url = QueryHelpers.AddQueryString(_oAuthServerEndPoint, queryParams);
            return url;
        }
        public async Task<TokenResult> ExchangeCodeOnTokenAsync(string code, string codeVerifier)
        {
            var authParams = new Dictionary<string, string>()
            {
                { "tenant", _tenant},
                { "client_id", _clientId},
                { "response_mode", "query"},
                { "code", code},
                { "redirect_uri", _redirectUrl},
                { "grant_type", "authorization_code"},
                { "code_verifier", codeVerifier},
                { "client_secret", _secret},
            };
            var tokenResult = await _httpClientHelper.SendPostRequestAsync<TokenResult>(_tokenEndPoint, authParams);
            return tokenResult;
        }
        public async Task<TokenResult> RefreshTokenAsync(string refreshToken)
        {
            var refreshParams = new Dictionary<string, string>()
            {
                { "tenant", _tenant},
                { "client_id", _clientId},
                { "response_mode", "query"},
                { "grant_type", "refresh_token"},
                { "refresh_token", refreshToken},
                { "client_secret", _secret}
            };
            var tokenResult = await _httpClientHelper.SendPostRequestAsync<TokenResult>(_refreshEndPoint, refreshParams);
            return tokenResult;
        }
    }
}
