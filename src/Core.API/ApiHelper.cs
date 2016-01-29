using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using eSIS.Core.API.Configuration;
using eSIS.Core.API.Exceptions;

namespace eSIS.Core.API
{
    internal static class ApiHelper
    {
        public static string GetRequiredHeader(string name)
        {
            if (HttpContext.Current.Request.Headers[name] != null)
            {
                return HttpContext.Current.Request.Headers.GetValues(name).First();
            }

            throw new RequiredHeaderException("Missing header: " + name);
        }

        public static string GetOptionalHeader(string name)
        {
            return HttpContext.Current.Request.Headers[name] != null ? HttpContext.Current.Request.Headers.GetValues(name).First() : null;
        }

        public static ApiClientConfig GetByClientToken(string token)
        {
            var clientSection = ConfigurationManager.GetSection("") as ApiClientSection;
            return clientSection.Clients.FirstOrDefault(p => p.Token == token);
        }

        public static List<ApiClientConfig> GetAllClients()
        {
            var clientSection = ConfigurationManager.GetSection("") as ApiClientSection;
            return clientSection.Clients.ToList();
        }
    }
}