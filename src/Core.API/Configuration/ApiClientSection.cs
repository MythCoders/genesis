using System.Configuration;

namespace eSIS.Core.API.Configuration
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