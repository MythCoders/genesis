using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using eSIS.Core.Classes;

// ReSharper disable Mvc.ViewNotResolved
// ReSharper disable VirtualMemberNeverOverriden.Global

namespace eSIS.Core.UI
{
    public class UiControllerCrudBase<T> : Controller
    {
        internal readonly WebApiClient _apiClient;
        internal readonly string _directoryPath;

        public UiControllerCrudBase(string directoryPath)
        {
            _apiClient = new WebApiClient();
            _directoryPath = directoryPath;
        }

        public virtual async Task<ActionResult> Index()
        {
            var url = new Url().SubDirectory(_directoryPath).Generate();
            return View(await _apiClient.MakeGetRequest<List<T>>(url));
        }

        public virtual async Task<ActionResult> Detail(int id)
        {
            var url = new Url().SubDirectory(_directoryPath).Method(id.ToString()).Generate();
            return View(await _apiClient.MakeGetRequest<T>(url));
        }
    }
}