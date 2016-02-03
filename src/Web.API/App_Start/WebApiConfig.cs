using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using eSIS.Core.API;
using eSIS.Core.API.Exceptions;

namespace eSIS.Web.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Add(new BrowserJsonFormatter());
            config.Services.Replace(typeof(IExceptionHandler), new ApiExceptionHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
