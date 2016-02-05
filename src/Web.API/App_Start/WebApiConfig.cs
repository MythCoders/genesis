using eSIS.Core;
using eSIS.Core.API;
using System.Web.Http;
using eSIS.Core.API.Exceptions;
using System.Net.Http.Formatting;

namespace eSIS.Web.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //config.Formatters.Add(new BrowserJsonFormatter());
            config.Filters.Add(new ApiExceptionFilterAttribute());

            var formatter = new JsonMediaTypeFormatter
            {
                Indent = !ConfigurationHelper.InstanceIsProduction,
                MaxDepth = ConfigurationHelper.InstanceIsProduction ? int.MaxValue : 100
            };

            config.Services.Replace(typeof (IContentNegotiator), new JsonContentNegotiator(formatter));

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
