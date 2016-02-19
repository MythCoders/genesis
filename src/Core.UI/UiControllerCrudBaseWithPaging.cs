using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using MC.eSIS.Core.Classes;
using MC.eSIS.Core.Entities;

namespace MC.eSIS.Core.UI
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