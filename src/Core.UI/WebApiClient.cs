using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;

namespace eSIS.Core.UI
{
    public class WebApiClient
    {
        private readonly Logger _logger;
        private readonly HttpClient _client;

        public WebApiClient()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add(Constants.ApiRequestHeaderName, ConfigurationHelper.InstanceApiAuthKey());
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<T> MakeGetRequest<T>(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                _logger.Warn("Get Request did not have url");
                throw new ArgumentException(nameof(url));
            }

            var callUri = new Uri(url, UriKind.Absolute);
            var response = await _client.GetAsync(callUri, HttpCompletionOption.ResponseHeadersRead);

            ProcessRequest(response);

            return await DeseralizeObject<T>(response.Content);
        }

        public async Task<TResult> MakeGetRequest<TResult, TRequest>(string url, TRequest data)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                _logger.Warn("Get Request did not have url");
                throw new ArgumentException(nameof(url));
            }

            var callUri = new Uri(url, UriKind.Absolute);
            var message = new HttpRequestMessage(HttpMethod.Get, callUri);

            var jsonData = JsonConvert.SerializeObject(data);
            message.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(message);

            ProcessRequest(response);

            return await DeseralizeObject<TResult>(response.Content);
        }

        public async Task<TResult> MakePostRequest<TResult, TRequest>(string url, TRequest data)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                _logger.Warn("Put Request did not have url");
                throw new ArgumentException(nameof(url));
            }

            var callUri = new Uri(url, UriKind.Absolute);

            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(callUri, content);

            ProcessRequest(response);

            return await DeseralizeObject<TResult>(response.Content);
        }

        public async Task<TResult> MakePutRequest<TResult, TRequest>(string url, TRequest data)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                _logger.Warn("Put Request did not have url");
                throw new ArgumentException(nameof(url));
            }

            var callUri = new Uri(url, UriKind.Absolute);

            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(callUri, content);

            ProcessRequest(response);

            return await DeseralizeObject<TResult>(response.Content);
        }

        public async Task<TResult> MakeDeleteRequest<TResult>(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                _logger.Warn("Delete Request did not have url");
                throw new ArgumentException(nameof(url));
            }

            var callUri = new Uri(url, UriKind.Absolute);
            var response = await _client.DeleteAsync(callUri);

            ProcessRequest(response);

            return await DeseralizeObject<TResult>(response.Content);
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

                _logger.Trace("Starting object serialization");
                var jsonObject = await Task.Run(() => serializer.Deserialize<T>(jsonReader));
                _logger.Trace("Finished");

                return jsonObject;
            }
        }

        /// <summary>
        /// Generates a unique id for a request to the Api. This can then be tracked
        /// in the Api
        /// </summary>
        /// <returns></returns>
        //private static string GetRequestId()
        //{
        //    //todo replace with users real identifier 
        //    return $"{Guid.NewGuid()}_{"test@email.com"}_{DateTime.Now.ToString("mmddyyyyHHmmss")}";
        //}

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

