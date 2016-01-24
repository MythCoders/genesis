using System.Configuration;

namespace eSIS.Core
{
    public static class ConfigurationHelper
    {
        public static string GetByKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
