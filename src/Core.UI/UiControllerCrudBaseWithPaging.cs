using System.Web.Mvc;
using Kendo.Mvc.UI;

namespace eSIS.Core.UI
{
    public class UiControllerCrudBaseWithPaging<T> : UiControllerCrudBase<T>
    {
        public UiControllerCrudBaseWithPaging(string directoryPath, string controllerName)
            : base(directoryPath, controllerName)
        { }

        public ActionResult Index_Query([DataSourceRequest] DataSourceRequest request)
        {
            //var url = new Url().SubDirectory(DirectoryPath).Method("Page").Generate();
            //var data = await ApiClient.MakeRawPostRequest(url, request);
            //var jsonStr = Encoding.UTF8.GetString(data);
            //return Json(Task.Factory.StartNew(() => JsonConvert.DeserializeObject<DataSourceResult>(jsonStr)));
            return Json(null);
        }
    }
}