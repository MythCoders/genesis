using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using MC.eSIS.Core.Classes;

namespace MC.eSIS.Core.UI
{
    public static class HttpClientExtensions
    {
        internal static ApiResponse<W> Post<T, W>(this HttpClient client, string uri, T request, Func<HttpResponseMessage, W> codeBlock)
        {
            var response = client.PostAsJsonAsync(uri, request).Result;

            if (response.IsSuccessStatusCode)
            {
                return ApiResponse<W>.Success(codeBlock(response));
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errors = response.Content.ReadAsAsync<List<PropertyError>>().Result;
                if (errors != null)
                {
                    return ApiResponse<W>.Error(errors);
                }
            }
            else
            {
                HandleError(response);
            }

            return null;
        }

        internal static ApiResponse Post<T>(this HttpClient client, string uri, T request, Action<HttpResponseMessage> codeBlock)
        {
            var response = client.PostAsJsonAsync(uri, request).Result;

            if (response.IsSuccessStatusCode)
            {
                codeBlock(response);
                return ApiResponse.Success();
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errors = response.Content.ReadAsAsync<List<PropertyError>>().Result;
                if (errors != null)
                {
                    return ApiResponse.Error(errors);
                }
            }
            else
            {
                HandleError(response);
            }

            return null;
        }

        internal static ApiResponse<T> Get<T>(this HttpClient client, string uri, Func<HttpResponseMessage, T> codeBlock)
        {
            var response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                return ApiResponse<T>.Success(codeBlock(response));
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errors = response.Content.ReadAsAsync<List<PropertyError>>().Result;
                if (errors != null)
                {
                    return ApiResponse<T>.Error(errors);
                }
            }
            else
            {
                HandleError(response);
            }

            return null;
        }

        internal static ApiResponse Get<T>(this HttpClient client, string uri, Action<HttpResponseMessage> codeBlock)
        {
            var response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                codeBlock(response);
                return ApiResponse.Success();
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errors = response.Content.ReadAsAsync<List<PropertyError>>().Result;
                if (errors != null)
                {
                    return ApiResponse.Error(errors);
                }
            }
            else
            {
                HandleError(response);
            }

            return null;
        }

        internal static void HandleError(HttpResponseMessage response)
        {
            var error = response.Content.ReadAsAsync<GeneralError>().Result;
            throw new ApiException(response.StatusCode, error.Message);
        }
    }
}