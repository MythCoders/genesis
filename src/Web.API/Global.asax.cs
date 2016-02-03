using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using eSIS.Core;
using NLog;

namespace eSIS.Web.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var logger = LogManager.GetCurrentClassLogger();

            logger.Trace($"Launching {ConfigurationHelper.SystemName} v{ConfigurationHelper.SystemVersion}");
            logger.Trace("Registering all areas");
            AreaRegistration.RegisterAllAreas();

            logger.Trace("Configuring WebApi");
            GlobalConfiguration.Configure(WebApiConfig.Register);

            logger.Trace("Configuring Filters");
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            logger.Trace("Configuring Routes");
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            logger.Trace("Configuring bundles");
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
