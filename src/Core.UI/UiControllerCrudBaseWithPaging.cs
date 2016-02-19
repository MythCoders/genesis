using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using MC.eSIS.Core.Classes;

namespace MC.eSIS.Core.UI
{
    public class UiControllerCrudBaseWithPaging<T> : UiControllerCrudBase<T>
    {
        public UiControllerCrudBaseWithPaging(string directoryPath, string controllerName)
            : base(directoryPath, controllerName)
        { }

        public async Task<ActionResult> Index_Query([DataSourceRequest] DataSourceRequest request)
        {
            var url = new Url().SubDirectory(DirectoryPath).Method("GetPage").Generate();
            return Json(await ApiClient.MakeGetRequest<DataSourceResult, DataSourceRequest>(url, request));
        }
    }
}