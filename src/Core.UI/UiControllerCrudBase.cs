using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using MC.eSIS.Core.Classes;
using MC.eSIS.Core.Entities;
using NLog;

// ReSharper disable Mvc.ViewNotResolved
// ReSharper disable VirtualMemberNeverOverriden.Global

namespace MC.eSIS.Core.UI
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
            ViewBag.Controller = controllerName;
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
            ViewBag.Action = "Create";

            var model = new T();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(T data)
        {
            ViewBag.Action = "Create";

            if (ModelState.IsValid)
            {
                var url = new Url().SubDirectory(DirectoryPath).Generate();
                var createdData = await ApiClient.MakePostRequest<T, T>(url, data);
                TempData["Success"] = "Created";
                return View("Detail", createdData);
            }

            return View(data);
        }

        public virtual async Task<ActionResult> Edit(int id)
        {
            ViewBag.Action = "Edit";

            var url = new Url().SubDirectory(DirectoryPath).Method(id.ToString()).Generate();
            return View(await ApiClient.MakeGetRequest<T>(url));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(T data)
        {
            ViewBag.Action = "Edit";
            TempData["Success"] = "Updated";

            if (ModelState.IsValid)
            {
                var url = new Url().SubDirectory(DirectoryPath).Method(data.Id.ToString()).Generate();
                await ApiClient.MakePutRequest<T, T>(url, data);
                return await Detail(data.Id);
            }

            return View(data);
        }
    }
}