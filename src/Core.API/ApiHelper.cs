using System.Collections.Generic;
using System.Linq;
using System.Web;
using MC.eSIS.Core.API.Exceptions;
using MC.eSIS.Core.Entities.Infrastructure;
using MC.eSIS.Database;

namespace MC.eSIS.Core.API
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

        public static ApiClient GetByClientToken(SisContext db, string token)
        {
            return db.ApiClients.SingleOrDefault(p => p.Token == token);
        }

        public static List<ApiClient> GetAllClients(SisContext db)
        {
            return db.ApiClients.ToList();
        }
    }
}