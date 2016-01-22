using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace eSIS.Core.Classes
{
    public class WebApiClient
    {
        public async Task<HttpContent> MakeGetRequest(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new Exception("The URL is null!");
            }

            var request = new HttpClient();
            var response = await request.GetAsync(new Uri(url, UriKind.Absolute));

            if (response.StatusCode == HttpStatusCode.ServiceUnavailable ||
                response.StatusCode == HttpStatusCode.BadGateway ||
                response.StatusCode == HttpStatusCode.GatewayTimeout ||
                response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception("Bad response!");
            }

            return response.Content;
        }

        public async Task<HttpContent> MakePostRequest(string url, IEnumerable<KeyValuePair<string, string>> postData)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new Exception("The URL is null!");
            }

            var request = new HttpClient();
            var response = await request.PostAsJsonAsync(new Uri(url, UriKind.Absolute), postData);
            
            if (response.StatusCode == HttpStatusCode.ServiceUnavailable ||
              response.StatusCode == HttpStatusCode.BadGateway ||
              response.StatusCode == HttpStatusCode.GatewayTimeout ||
              response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception("Bad response!");
            }

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
    }
}
