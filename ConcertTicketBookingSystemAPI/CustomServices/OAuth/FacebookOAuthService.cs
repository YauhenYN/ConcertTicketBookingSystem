using ConcertTicketBookingSystemAPI.CustomServices.EmailSending;
using ConcertTicketBookingSystemAPI.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.CustomServices.OAuth
{
    public class FacebookOAuthService
    {
        private readonly string _clientId;
        private readonly string _secret;
        private readonly string _oAuthServerEndPoint;
        private readonly string _tokenEndPoint;
        private readonly string _redirectUrl;
        private readonly string _scope;
        public FacebookOAuthService(string clientId, string secret, string oAuthServerEndPoint, string tokenEndPoint, string redirectUrl, string scope)
        {
            _clientId = clientId;
            _secret = secret;
            _oAuthServerEndPoint = oAuthServerEndPoint;
            _tokenEndPoint = tokenEndPoint;
            _redirectUrl = redirectUrl;
            _scope = scope;
        }
        public string GenerateOAuthRequstUrl(string state)
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
            var tokenResult = await HttpClientHelper.SendPostRequest<TokenResult>(_tokenEndPoint, authParams);
            return tokenResult;
        }
        public async Task<dynamic> GetUserCredentialsAsync(string accessToken)
        {
            var response = await HttpClientHelper.SendGetRequest<dynamic>("https://graph.facebook.com/v13.0/me?fields=id,name,email", null, accessToken);
            return response;
        }
    }
}
