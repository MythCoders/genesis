using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using eSIS.Core.Exceptions;

namespace eSIS.Core.API.Exceptions
{
    public class ApiErrorResult : HttpResponseMessage
    {
        private readonly HttpStatusCode _code;
        private readonly ApiErrorExcpetion _excpetion;

        public ApiErrorResult(ApiErrorExcpetion excpetion, HttpStatusCode code)
        {
            _code = code;
            _excpetion = excpetion;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var response = new HttpResponseMessage(_code);
            response.Content = new ObjectContent(typeof (ApiErrorExcpetion), _excpetion, new JsonMediaTypeFormatter());
            return Task.FromResult(response);
        }
    }
}