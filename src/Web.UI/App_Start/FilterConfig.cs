using System.Web.Mvc;
using eSIS.Core.UI;
using eSIS.Core.UI.Attributes;

namespace eSIS.Web.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UiHandleErrorAttribute());
            filters.Add(new StatusMessageAttribute());
        }
    }
}
