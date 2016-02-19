using System.Globalization;
using System.Net.Http.Formatting;
using System.Web.Http;
using MC.eSIS.Core.API;
using MC.eSIS.Core.API.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

            ConfigureJson(config);
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

        private static void ConfigureJson(HttpConfiguration config)
        {
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            json.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            json.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            json.SerializerSettings.Culture = new CultureInfo("en-US");
            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            var formatter = new JsonMediaTypeFormatter();
            formatter.Indent = true;
            formatter.MaxDepth = 100;
            config.Services.Replace(typeof (IContentNegotiator), new JsonContentNegotiator(formatter));
        }
    }
}
