using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eSIS.Core.API
{
    public class LoggingHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LogRequestLoggingInfo(request);

            return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                var response = task.Result;
                LogResponseLoggingInfo(response);
                return response;
            }, cancellationToken);
        }

        private static void LogRequestLoggingInfo(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                request.Content.ReadAsByteArrayAsync()
                    .ContinueWith(task =>
                    {
                        var result = Encoding.UTF8.GetString(task.Result);
                    })
                    .Wait();
            }
        }

        private static void LogResponseLoggingInfo(HttpResponseMessage response)
        {
            if (response.Content != null)
            {
                response.Content.ReadAsByteArrayAsync()
                    .ContinueWith(task =>
                    {
                        var responseMsg = Encoding.UTF8.GetString(task.Result);
                    });
            }
        }
    }
}