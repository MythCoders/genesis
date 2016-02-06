using System;
using System.Configuration;

namespace eSIS.Core
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
    }
}
