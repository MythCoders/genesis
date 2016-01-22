using eSIS.Core.Entities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace eSIS.Web.API.Classes
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