using System.Threading.Tasks;
using System.Web.Mvc;
using eSIS.Core.Classes;
using Kendo.Mvc.UI;

namespace eSIS.Core.UI
{
    public class UiControllerCrudBaseWithPaging<T> : UiControllerCrudBase<T>
    {
        public UiControllerCrudBaseWithPaging(string directoryPath)
            : base(directoryPath)
        { }

        public async Task<ActionResult> Index_Query([DataSourceRequest] DataSourceRequest request)
        {
            var url = new Url().SubDirectory(_directoryPath).Method("").Generate();
            return Json(await _apiClient.MakeGetRequest<DataSourceResult>(url));
        }
    }
}