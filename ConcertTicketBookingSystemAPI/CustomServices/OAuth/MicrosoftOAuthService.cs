using ConcertTicketBookingSystemAPI.CustomServices.EmailSending;
using ConcertTicketBookingSystemAPI.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.CustomServices.OAuth
{
    public class MicrosoftOAuthService
    {
        private readonly string _clientId;
        private readonly string _secret;
        private readonly string _oAuthServerEndPoint;
        private readonly string _tokenEndPoint;
        private readonly string _refreshEndPoint;
        private readonly string _redirectUrl;
        private readonly string _tenant;
        public MicrosoftOAuthService(string tenant, string clientId, string secret, string oAuthServerEndPoint, string tokenEndPoint, string refreshEndPoint, string redirectUrl)
        {
            _clientId = clientId;
            _secret = secret;
            _oAuthServerEndPoint = oAuthServerEndPoint;
            _tokenEndPoint = tokenEndPoint;
            _refreshEndPoint = refreshEndPoint;
            _redirectUrl = redirectUrl;
            _tenant = tenant;
        }
        public string GenerateOAuthRequstUrl(string scope, string codeChallenge)
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
            var tokenResult = await HttpClientHelper.SendPostRequest<TokenResult>(_tokenEndPoint, authParams);
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
            var tokenResult = await HttpClientHelper.SendPostRequest<TokenResult>(_refreshEndPoint, refreshParams);
            return tokenResult;
        }
        public async Task<dynamic> GetUserCredentialsAsync(string accessToken)
        { 
            var response = await HttpClientHelper.SendGetRequest<dynamic>("https://graph.microsoft.com/v1.0/me", null, accessToken);
            return response;
        }
    }
}
