using HttpClientHelper;
using OAuth.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OAuth
{
    public class FacebookOAuthService : OAuthServiceBase, IFacebookOAuthService
    {
        private readonly string _scope;
        public FacebookOAuthService(HttpClientHelperBase httpClientHelper,
            string clientId, 
            string secret, 
            string oAuthServerEndPoint, 
            string tokenEndPoint, 
            string redirectUrl, 
            string scope)
            : base(httpClientHelper, clientId, secret, oAuthServerEndPoint, tokenEndPoint, redirectUrl, "https://graph.facebook.com/v13.0/me?fields=id,name,email")
        {
            _scope = scope;
        }
        public string GenerateOAuthRequestUrl(string state)
        {
            var queryParams = new Dictionary<string, string>()
            {
                { "client_id", _clientId},
                { "redirect_uri", _redirectUrl},
                { "state", state},
                { "scope", _scope }
            };
            var url = QueryHelpers.AddQueryString(_oAuthServerEndPoint, queryParams);
            return url;
        }
        public async Task<TokenResult> ExchangeCodeOnTokenAsync(string code)
        {
            var authParams = new Dictionary<string, string>()
            {
                { "client_id", _clientId},
                { "redirect_uri", _redirectUrl},
                { "client_secret", _secret},
                { "code", code}
            };
            var tokenResult = await _httpClientHelper.SendPostRequestAsync<TokenResult>(_tokenEndPoint, authParams);
            return tokenResult;
        }
    }
}
