using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using eSIS.Core.Classes;
using eSIS.Core.Entities;
using Kendo.Mvc.UI;

namespace eSIS.Core.UI
{
    public class UiControllerCrudBaseWithPaging<T> : UiControllerCrudBase<T> 
        where T : BaseEntity, new()
    {
        public UiControllerCrudBaseWithPaging(string directoryPath, string controllerName)
            : base(directoryPath, controllerName)
        { }

        public async Task<ActionResult> Index_Query([DataSourceRequest] DataSourceRequest request)
        {
            var url = new Url().SubDirectory(DirectoryPath).Method("Page").Generate();
            var data = await ApiClient.MakeRawPostRequest(url, request);
            var jsonStr = Encoding.UTF8.GetString(data);
            return Content(jsonStr, "application/json", Encoding.Default);
        }
    }
}