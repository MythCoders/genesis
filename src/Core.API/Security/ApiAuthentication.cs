using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Threading;
using System.Web.Http.Filters;

namespace eSIS.Core.API.Security
{
    public class ApiAuthentication : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get { return true; } }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            IEnumerable<string> apiKeyHeaderValues = null;

            if (context.Request.Headers.TryGetValues("X-API-Key", out apiKeyHeaderValues))
            {
                var apiToken = apiKeyHeaderValues.First();
                var client = ApiHelper.GetByClientToken(apiToken);
                var claim = new Claim(ClaimTypes.Name, client.ClientName);
                var identity = new ClaimsIdentity(new[] { claim }, "X-API-Key");
                var principal = new ClaimsPrincipal(identity);
                context.Principal = principal;
            }

            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Result);
        }
    }
}
