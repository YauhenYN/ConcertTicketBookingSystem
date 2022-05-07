using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientHelper
{
    public class HttpClientHelper : HttpClientHelperBase
    {
        public override async Task<T> SendGetRequestAsync<T>(string endpoint, Dictionary<string, string> queryParams, string accessToken)
        {
            return await SendHttpRequestAsync<T>(HttpMethod.Get, endpoint, accessToken, queryParams);
        }

        public override async Task<T> SendPostRequestAsync<T>(string endpoint, Dictionary<string, string> bodyParams)
        {
            var httpContent = new FormUrlEncodedContent(bodyParams);
            return await SendHttpRequestAsync<T>(HttpMethod.Post, endpoint, httpContent: httpContent);
        }

        public override async Task SendPutRequestAsync(string endpoint, Dictionary<string, string> queryParams, object body, string accessToken)
        {
            var bodyJson = JsonConvert.SerializeObject(body);
            var httpContent = new StringContent(bodyJson, Encoding.UTF8, "application/json");
            await SendHttpRequestAsync<dynamic>(HttpMethod.Put, endpoint, accessToken, queryParams, httpContent);
        }
    }
}
