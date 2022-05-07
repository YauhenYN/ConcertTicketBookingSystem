using HttpClientHelper;
using System.Threading.Tasks;

namespace OAuth
{
    public abstract class OAuthServiceBase
    {
        protected readonly HttpClientHelperBase _httpClientHelper;
        protected readonly string _clientId;
        protected readonly string _secret;
        protected readonly string _oAuthServerEndPoint;
        protected readonly string _tokenEndPoint;
        protected readonly string _redirectUrl;
        protected readonly string _credentialsEndPoint;
        public OAuthServiceBase(HttpClientHelperBase httpClientHelper,
            string clientId, 
            string secret, 
            string oAuthServerEndPoint, 
            string tokenEndPoint,
            string redirectUrl, string credentialsEndPoint)
        {
            _httpClientHelper = httpClientHelper;
            _clientId = clientId;
            _secret = secret;
            _oAuthServerEndPoint = oAuthServerEndPoint;
            _tokenEndPoint = tokenEndPoint;
            _redirectUrl = redirectUrl;
            _credentialsEndPoint = credentialsEndPoint;
        }
        public async Task<dynamic> GetUserCredentialsAsync(string accessToken)
        {
            var response = await _httpClientHelper.SendGetRequestAsync<dynamic>(_credentialsEndPoint, null, accessToken);
            return response;
        }
    }
}
