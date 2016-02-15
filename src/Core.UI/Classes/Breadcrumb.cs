using System.Web.Routing;

namespace eSIS.Core.UI.Classes
{
    public class Breadcrumb
    {
        public string Name { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public RouteValueDictionary Routes { get; set; }
    }
}
