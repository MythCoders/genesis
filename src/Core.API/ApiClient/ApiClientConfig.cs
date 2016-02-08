using System.Configuration;

namespace eSIS.Core.API.ApiClient
{
    public class ApiClientConfig : ConfigurationElement
    {
        public ApiClientConfig() { }

        public ApiClientConfig(string token, string clientName)
        {
            Token = token;
            ClientName = clientName;
        }

        [ConfigurationProperty("token", IsRequired = true, IsKey = true)]
        public string Token
        {
            get { return (string)this["token"]; }
            set { this["token"] = value; }
        }

        [ConfigurationProperty("clientName", IsRequired = true, IsKey = false)]
        public string ClientName
        {
            get { return (string)this["clientName"]; }
            set { this["clientName"] = value; }
        }

        [ConfigurationProperty("isActive", IsRequired = true, IsKey = false)]
        public bool IsActive
        {
            get { return (bool)this["isActive"]; }
            set { this["isActive"] = value; }
        }
    }
}