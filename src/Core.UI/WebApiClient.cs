using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        public async Task<T> MakeGetRequest<T>(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException(nameof(url));
            }

            var callUri = new Uri(url, UriKind.Absolute);
            var response = await _client.GetAsync(callUri, HttpCompletionOption.ResponseHeadersRead);

            ProcessRequest(response);

            return await DeseralizeObject<T>(response.Content);
        }

        public async Task<T> MakePostRequest<T>(string url, IEnumerable<KeyValuePair<string, string>> data)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException(nameof(url));
            }
            
            var callUri = new Uri(url, UriKind.Absolute);
            var content = new FormUrlEncodedContent(data);
            var response = await _client.PostAsync(callUri, content);

            ProcessRequest(response);

            return await DeseralizeObject<T>(response.Content);
        }

        public async Task<T> MakeDeleteRequest<T>(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException(nameof(url));
            }

            var callUri = new Uri(url, UriKind.Absolute);
            var response = await _client.DeleteAsync(callUri);

            ProcessRequest(response);

            return await DeseralizeObject<T>(response.Content);
        }

        public async Task<T> MakePutRequest<T>(string url, IEnumerable<KeyValuePair<string, string>> data)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException(nameof(url));
            }

            var callUri = new Uri(url, UriKind.Absolute);
            var content = new FormUrlEncodedContent(data);
            var response = await _client.PutAsync(callUri, content);

            ProcessRequest(response);

            return await DeseralizeObject<T>(response.Content);
        }

        public static async Task<T> DeseralizeObject<T>(HttpContent content)
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
            //todo replace with users real identifier 
            return $"{Guid.NewGuid()}_{"test@email.com"}_{DateTime.Now.ToString("mmddyyyyHHmmss")}";
        }

        private static void ProcessRequest(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                //todo provide more information in exceptions details
                Debugger.Break();
                throw new Exception($"Error! Currently Unknown.");
            }
        }
    }
}

