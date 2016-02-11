using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Threading;
using System.Web.Http.Filters;
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

                var apiToken = apiKeyHeaderValues.First();
                var client = ApiHelper.GetByClientToken(apiToken);
                var claim = new Claim(ClaimTypes.Name, client.ClientName);
                var identity = new ClaimsIdentity(new[] { claim }, Constants.ApiRequestKeyHeaderName);
                var principal = new ClaimsPrincipal(identity);
                context.Principal = principal;
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
