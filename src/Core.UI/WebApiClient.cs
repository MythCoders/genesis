using System;
using System.Diagnostics;
using System.IO;
using System.Net;
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
            _client.DefaultRequestHeaders.Add(Constants.ApiRequestKeyHeaderName, ConfigurationHelper.InstanceApiAuthKey());
            _client.DefaultRequestHeaders.Add(Constants.ApiRequestClientNameHeaderName, ConfigurationHelper.InstanceName());
            _client.DefaultRequestHeaders.Add(Constants.ApiRequestClientInProductionHeaderName, ConfigurationHelper.InstanceIsProduction().ToString());
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _logger = LogManager.GetCurrentClassLogger();
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

        #region Get

        public async Task<byte[]> MakeRawGetRequest(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                _logger.Warn("Get Request did not have url");
                throw new ArgumentException(nameof(url));
            }

            var callUri = new Uri(url, UriKind.Absolute);
            var response = await _client.GetAsync(callUri);

            ProcessRequest(response);
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<T> MakeGetRequest<T>(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                _logger.Warn("Get Request did not have url");
                throw new ArgumentException(nameof(url));
            }

            var callUri = new Uri(url, UriKind.Absolute);
            var response = await _client.GetAsync(callUri);

            ProcessRequest(response);
            return await DeseralizeObject<T>(response.Content);
        }

        #endregion

        #region Post

        public async Task<byte[]> MakeRawPostRequest<TRequest>(string url, TRequest data)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                _logger.Warn("RawPut Request did not have url");
                throw new ArgumentException(nameof(url));
            }

            var callUri = new Uri(url, UriKind.Absolute);

            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(callUri, content);

            ProcessRequest(response);
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<TResult> MakePostRequest<TResult, TRequest>(string url, TRequest data)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                _logger.Warn("RawPut Request did not have url");
                throw new ArgumentException(nameof(url));
            }

            var callUri = new Uri(url, UriKind.Absolute);

            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(callUri, content);

            ProcessRequest(response);
            return await DeseralizeObject<TResult>(response.Content);
        }

        #endregion

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

        private static void ProcessRequest(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                Debugger.Break();
                throw new Exception($"Error! {response.Content}");
            }
        }
    }

    public class ApiResult
    {
        public HttpStatusCode StatusCode { get; set; }
    }
}
