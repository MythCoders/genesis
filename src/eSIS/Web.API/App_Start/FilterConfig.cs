using System.Web.Mvc;
using eSIS.Core.API;

namespace eSIS.Web.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandlerAttribute());
        }
    }
}
