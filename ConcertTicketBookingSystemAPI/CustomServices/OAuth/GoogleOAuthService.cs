using ConcertTicketBookingSystemAPI.CustomServices.EmailSending;
using ConcertTicketBookingSystemAPI.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.CustomServices.OAuth
{
    public class GoogleOAuthService
    {
        private readonly string _clientId;
        private readonly string _secret;
        private readonly string _oAuthServerEndPoint;
        private readonly string _tokenEndPoint;
        private readonly string _refreshEndPoint;
        private readonly string _googleApiEndPoint;
        public GoogleOAuthService(string clientId, string secret, string oAuthServerEndPoint, string tokenEndPoint, string refreshEndPoint, string googleApiEndPoint)
        {
            _clientId = clientId;
            _secret = secret;
            _oAuthServerEndPoint = oAuthServerEndPoint;
            _tokenEndPoint = tokenEndPoint;
            _refreshEndPoint = refreshEndPoint;
            _googleApiEndPoint = googleApiEndPoint;
        }
        public string GenerateOAuthRequstUrl(string scope, string redirectUrl, string codeChallenge)
        {
            var queryParams = new Dictionary<string, string>()
            {
                { "client_id", _clientId},
                { "redirect_uri", redirectUrl},
                { "response_type", "code"},
                { "scope", scope},
                { "code_challenge", codeChallenge},
                { "code_challenge_method", "S256" },
                { "access_type", "offline" }
            };
            var url = QueryHelpers.AddQueryString(_oAuthServerEndPoint, queryParams);
            return url;
        }
        public async Task<TokenResult> ExchangeCodeOnTokenAsync(string code, string codeVerifier, string redirectUrl)
        {
            var authParams = new Dictionary<string, string>()
            {
                { "client_id", _clientId},
                { "client_secret", _secret},
                { "code", code},
                { "code_verifier", codeVerifier},
                { "grant_type", "authorization_code"},
                { "redirect_uri", redirectUrl}
            };
            var tokenResult = await HttpClientHelper.SendPostRequest<TokenResult>(_tokenEndPoint, authParams);
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
            var tokenResult = await HttpClientHelper.SendPostRequest<TokenResult>(_refreshEndPoint, refreshParams);
            return tokenResult;
        }
        public async Task<dynamic> GetUserCredentialsAsync(string accessToken)
        { 
            var response = await HttpClientHelper.SendGetRequest<dynamic>(_googleApiEndPoint + "/oauth2/v2/userinfo", null, accessToken);
            return response;
        }
    }
}
