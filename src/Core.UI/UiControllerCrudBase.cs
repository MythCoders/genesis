using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using eSIS.Core.Classes;
using eSIS.Core.Entities;
using NLog;

// ReSharper disable Mvc.ViewNotResolved
// ReSharper disable VirtualMemberNeverOverriden.Global

namespace eSIS.Core.UI
{
    public class UiControllerCrudBase<T> : Controller 
        where T : BaseEntity, new()
    {
        // ReSharper disable once MemberCanBeProtected.Global
        public readonly WebApiClient ApiClient;
        public readonly string DirectoryPath;
        // ReSharper disable once MemberCanBePrivate.Global
        public readonly Logger Logger;

        public UiControllerCrudBase(string directoryPath, string controllerName)
        {
            Logger = LogManager.GetLogger(controllerName);
            ApiClient = new WebApiClient();
            DirectoryPath = directoryPath;
        }

        public virtual async Task<ActionResult> Index()
        {
            var url = new Url().SubDirectory(DirectoryPath).Generate();
            return View(await ApiClient.MakeGetRequest<List<T>>(url));
        }

        public virtual async Task<ActionResult> Detail(int id)
        {
            var url = new Url().SubDirectory(DirectoryPath).Method(id.ToString()).Generate();
            return View(await ApiClient.MakeGetRequest<T>(url));
        }

        public virtual ActionResult Create()
        {
            var model = new T();
            return View(model);
        }

        public virtual async Task<ActionResult> Edit(int id)
        {
            var url = new Url().SubDirectory(DirectoryPath).Generate();
            return View(await ApiClient.MakeGetRequest<T>($"{url}/{id}"));
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public virtual async Task<ActionResult> Edit(T data)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var url = new Url().SubDirectory(DirectoryPath).Generate();

        //        await ApiClient.MakePutRequest<>();
        //    }


            
        //    return View(await ApiClient.MakeGetRequest<T>($"{url}/{id}"));
        //}
    }
}