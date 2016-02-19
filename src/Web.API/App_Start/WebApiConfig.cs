using System.Net.Http.Formatting;
using System.Web.Http;
using MC.eSIS.Core;
using MC.eSIS.Core.API;
using MC.eSIS.Core.API.Exceptions;

namespace MC.eSIS.Web.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new ApiExceptionFilterAttribute());
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = ConfigurationHelper.GetJsonSerializerSettings();
            config.Services.Replace(typeof (IContentNegotiator), new JsonContentNegotiator(ConfigurationHelper.GetJsonMediaTypeFormatter()));

            ConfigureRoutes(config);
        }

        private static void ConfigureRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
                );
        }
    }
}
