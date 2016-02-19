using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using MC.eSIS.Core.Classes;
using MC.eSIS.Core.Entities;
using MC.eSIS.Core.UI;

namespace MC.eSIS.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            var url = new Url().SubDirectory("District").Generate();
            var client = new WebApiClient();
            return View(await client.MakeGetRequest<List<District>>(url));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}