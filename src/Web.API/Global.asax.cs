using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;

namespace eSIS.Web.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            Logger.Trace("Registering all areas");
            AreaRegistration.RegisterAllAreas();

            Logger.Trace("Configuring WebApi");
            GlobalConfiguration.Configure(WebApiConfig.Register);

            Logger.Trace("Configuring Filters");
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            Logger.Trace("Configuring Routes");
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Logger.Trace("Configuring bundles");
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
