using eSIS.Core.Entities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace eSIS.Core.API
{
    public class ServiceCrudBaseWithPaging<T> : ServiceCrudBase<T>
        where T : BaseEntity
    {
        public DataSourceResult GetPage(DataSourceRequest request)
        {
            return Database.Set<T>().ToDataSourceResult(request);
        }
    }
}