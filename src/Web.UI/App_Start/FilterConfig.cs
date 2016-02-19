using System.Web.Mvc;
using MC.eSIS.Core.UI;
using MC.eSIS.Core.UI.Attributes;

namespace MC.eSIS.Web.UI
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
