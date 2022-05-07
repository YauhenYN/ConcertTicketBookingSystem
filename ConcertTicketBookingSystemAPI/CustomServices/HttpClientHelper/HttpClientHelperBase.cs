using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientHelper
{
    public abstract class HttpClientHelperBase
    {
        public abstract Task<T> SendGetRequestAsync<T>(string endpoint, Dictionary<string, string> queryParams, string accessToken);
        public abstract Task<T> SendPostRequestAsync<T>(string endpoint, Dictionary<string, string> bodyParams);
        public abstract Task SendPutRequestAsync(string endpoint, Dictionary<string, string> queryParams, object body, string accessToken);
        protected async Task<T> SendHttpRequestAsync<T>(HttpMethod httpMethod, string endpoint, string accessToken = null, Dictionary<string, string> queryParams = null, HttpContent httpContent = null)
        {
            var url = queryParams != null
                ? QueryHelpers.AddQueryString(endpoint, queryParams)
                : endpoint;

            var request = new HttpRequestMessage(httpMethod, url);

            if (accessToken != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            if (httpContent != null)
            {
                request.Content = httpContent;
            }

            using var httpClient = new HttpClient();
            using var response = await httpClient.SendAsync(request);

            var resultJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(resultJson);
            }

            var result = JsonConvert.DeserializeObject<T>(resultJson);
            return result;
        }
    }
}
