using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace eSIS.Core.UI
{
    public class WebApiClient
    {
        private readonly HttpClient _client;

        public WebApiClient()
        {
            _client = new HttpClient();
            
            _client.DefaultRequestHeaders.Add("ClientAuthKey", ConfigurationHelper.GetByKey("ApiAuthKey"));
            _client.DefaultRequestHeaders.Add("RequestId", GetRequestId());
            _client.DefaultRequestHeaders.Add("User-Agent", "eSIS");
        }

        public async Task<HttpContent> MakeGetRequest(string url, IEnumerable<KeyValuePair<string, string>> getData)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException(nameof(url));
            }

            //TODO: Finish this
            var callUri = new Uri(url, UriKind.Absolute);
            var content = new FormUrlEncodedContent(getData);
            var response = await _client.GetAsync(callUri, HttpCompletionOption.ResponseHeadersRead);

            ProcessRequest(response);

            return response.Content;
        }

        public async Task<HttpContent> MakePostRequest(string url, IEnumerable<KeyValuePair<string, string>> postData)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException(nameof(url));
            }
            
            var callUri = new Uri(url, UriKind.Absolute);
            var content = new FormUrlEncodedContent(postData);
            var response = await _client.PostAsync(callUri, content);

            ProcessRequest(response);

            return response.Content;
        }

        public async Task<T> DeseralizeObject<T>(HttpContent content)
        {
            // NOTE!! We are really careful not to use a string here so we don't have to allocate a huge string.
            var inputStream = await content.ReadAsStreamAsync();

            using (var reader = new StreamReader(inputStream))
            using (JsonReader jsonReader = new JsonTextReader(reader))
            {
                // Parse the Json as an object
                var serializer = new JsonSerializer();
                var jsonObject = await Task.Run(() => serializer.Deserialize<T>(jsonReader));
                return jsonObject;
            }
        }

        /// <summary>
        /// Generates a unique id for a request to the Api. This can then be tracked
        /// in the Api
        /// </summary>
        /// <returns></returns>
        private static string GetRequestId()
        {
            return $"{Guid.NewGuid()}_{"justin.adkins@outlook.com"}_{DateTime.Now.ToString("G")}";
        }

        private static void ProcessRequest(HttpResponseMessage response)
        {
            if (response.StatusCode.IsIn(HttpStatusCode.ServiceUnavailable, HttpStatusCode.BadGateway, HttpStatusCode.GatewayTimeout, HttpStatusCode.InternalServerError))
            {
                throw new Exception("Bad data");
            }
        }
    }
}

