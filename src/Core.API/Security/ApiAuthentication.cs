using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Threading;
using System.Web.Http.Filters;
using eSIS.Database;
using NLog;

namespace eSIS.Core.API.Security
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiAuthentication : Attribute, IAuthenticationFilter
    {
        private readonly Logger _logger = LogManager.GetLogger("ApiAuthentication");
        public bool AllowMultiple { get { return true; } }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            _logger.Trace("Starting authentication");

            IEnumerable<string> apiKeyHeaderValues;

            if (context.Request.Headers.TryGetValues(Constants.ApiRequestKeyHeaderName, out apiKeyHeaderValues))
            {
                _logger.Trace("Header found");

                var dbContext = new SisContext("ApiAuthentication", "");

                var apiToken = apiKeyHeaderValues.First();
                var client = ApiHelper.GetByClientToken(dbContext, apiToken);

                if (client != null)
                {
                    _logger.Trace("Client found");

                    if (client.IsActive)
                    {
                        var claim = new Claim(ClaimTypes.Name, client.Name);
                        var identity = new ClaimsIdentity(new[] { claim }, Constants.ApiRequestKeyHeaderName);
                        var principal = new ClaimsPrincipal(identity);
                        context.Principal = principal;
                    }
                    else
                    {
                        _logger.Warn("Request denied for client {0}. Client is not active!", client.Name);
                    }
                }
                else
                {
                    _logger.Warn("Request denied for auth token {0}.", apiToken);
                }
            }

            _logger.Trace("Finishing authentication");

            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Result);
        }
    }
}
