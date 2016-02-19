using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MC.eSIS.Core.API.ApiClient
{
    public class ApiClientCollection : ConfigurationElementCollection, IEnumerable<ApiClientConfig>
    {
        public ApiClientCollection()
        {

        }

        public ApiClientConfig this[int index]
        {
            get { return (ApiClientConfig)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(ApiClientConfig serviceConfig)
        {
            BaseAdd(serviceConfig);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ApiClientConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ApiClientConfig)element).Token;
        }

        public void Remove(ApiClientConfig serviceConfig)
        {
            BaseRemove(serviceConfig.Token);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public new IEnumerator<ApiClientConfig> GetEnumerator()
        {
            return BaseGetAllKeys().Select(key => (ApiClientConfig)BaseGet(key)).GetEnumerator();
        }
    }
}
