using System;
using System.Configuration;
using System.Globalization;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MC.eSIS.Core
{
    public static class ConfigurationHelper
    {
        public static string SystemName()
        {
            return GetByKey<string>("System.Name");
        }

        public static string SystemVersion()
        {
            return GetByKey<string>("System.Version");
        }

        public static string InstanceName()
        {
            return GetByKey<string>("Instance.Name");
        }

        public static bool InstanceIsProduction()
        {
            return GetByKey<bool>("Instance.IsProduction");
        }

        public static string InstanceDbConnectionName()
        {
             return GetByKey<string>("Instance.DbConnectionName");
        }

        public static bool InstanceDbIsLogging()
        {
            return GetByKey<bool>("Instance.DbIsLogging");
        }

        public static string InstanceDbLogFilePath()
        {
            return GetByKey<string>("Instance.DbLogFilePath");
        }

        public static string InstanceApiAuthKey()
        {
            return GetByKey<string>("Instance.ApiAuthorizationKey");
        }

        public static string InstanceApiBaseUrl()
        {
            return GetByKey<string>("Instance.ApiBaseUrl");
        }

        private static T GetByKey<T>(string key)
        {
            var value = ConfigurationManager.AppSettings[key];

            if (value == null)
            {
                
            }

            return (T) Convert.ChangeType(value, typeof (T));
        }

        public static JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                Culture = new CultureInfo("en-US"),
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };
        }

        public static JsonMediaTypeFormatter GetJsonMediaTypeFormatter()
        {
            return new JsonMediaTypeFormatter
            {
                Indent =  true,
                MaxDepth = 100
            };
        }
    }
}
