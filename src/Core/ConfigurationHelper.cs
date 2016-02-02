using System;
using System.Configuration;

namespace eSIS.Core
{
    public enum SystemLoggingLevelEnum
    {
        None,
        Low,
        High
    }

    public static class ConfigurationHelper
    {
        public static readonly string SystemName = GetByKey<string>("System.Name");
        public static readonly string SystemVersion = GetByKey<string>("System.Version");
        public static readonly string InstanceName = GetByKey<string>("Instance.Name");
        public static readonly bool InstanceIsProduction = GetByKey<bool>("Instance.IsProduction");
        public static readonly string InstanceDbConnectionName = GetByKey<string>("Instance.DbConnectionName");
        public static readonly bool InstanceDbIsLogging = GetByKey<bool>("Instance.DbIsLogging");
        public static readonly string InstanceDbLogFilePath = GetByKey<string>("Instance.DbLogFilePath");
        public static readonly string InstanceApiAuthKey = GetByKey<string>("Instance.ApiAuthorizationKey");
        public static readonly string InstanceApiBaseUrl = GetByKey<string>("Instance.ApiBaseUrl");

        private static T GetByKey<T>(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            return (T) Convert.ChangeType(value, typeof (T));
        }
    }
}
