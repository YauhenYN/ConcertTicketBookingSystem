using HttpClientHelper;
using OAuth.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OAuth
{
    public class GoogleOAuthService : OAuthServiceBase, IGoogleOAuthService
    {
        protected readonly string _refreshEndPoint;
        public GoogleOAuthService(HttpClientHelperBase httpClientHelper,
            string clientId,
            string secret,
            string oAuthServerEndPoint,
            string tokenEndPoint,
            string refreshEndPoint,
            string redirectUrl)
            : base(httpClientHelper, clientId, secret, oAuthServerEndPoint, tokenEndPoint, redirectUrl, "https://www.googleapis.com/oauth2/v2/userinfo")
        {
            _refreshEndPoint = refreshEndPoint;
        }

        public string GenerateOAuthRequestUrl(string scope, string codeChallenge)
        {
            var queryParams = new Dictionary<string, string>()
            {
                { "client_id", _clientId},
                { "redirect_uri", _redirectUrl},
                { "response_type", "code"},
                { "scope", scope},
                { "code_challenge", codeChallenge},
                { "code_challenge_method", "S256" },
                { "access_type", "offline" }
            };
            var url = QueryHelpers.AddQueryString(_oAuthServerEndPoint, queryParams);
            return url;
        }
        public async Task<TokenResult> ExchangeCodeOnTokenAsync(string code, string codeVerifier)
        {
            var authParams = new Dictionary<string, string>()
            {
                { "client_id", _clientId},
                { "client_secret", _secret},
                { "code", code},
                { "code_verifier", codeVerifier},
                { "grant_type", "authorization_code"},
                { "redirect_uri", _redirectUrl}
            };
            var tokenResult = await _httpClientHelper.SendPostRequestAsync<TokenResult>(_tokenEndPoint, authParams);
            return tokenResult;
        }
        public async Task<TokenResult> RefreshTokenAsync(string refreshToken)
        {
            var refreshParams = new Dictionary<string, string>()
            {
                { "client_id", _clientId},
                { "client_secret", _secret},
                { "grant_type", "refresh_token"},
                { "refresh_token", refreshToken}
            };
            var tokenResult = await _httpClientHelper.SendPostRequestAsync<TokenResult>(_refreshEndPoint, refreshParams);
            return tokenResult;
        }
    }
}
