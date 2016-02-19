using System.Configuration;

namespace MC.eSIS.Core.API.ApiClient
{
    public class ApiClientSection : ConfigurationSection
    {
        [ConfigurationProperty("Clients", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ApiClientCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public ApiClientCollection Clients
        {
            get
            {
                return (ApiClientCollection)base["Clients"];
            }
        }
    }
}