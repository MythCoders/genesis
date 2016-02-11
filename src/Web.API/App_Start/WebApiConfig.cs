using System.Globalization;
using System.Net.Http.Formatting;
using eSIS.Core.API;
using System.Web.Http;
using eSIS.Core;
using eSIS.Core.API.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace eSIS.Web.API
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
