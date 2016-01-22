using System.Web.Mvc;
using eSIS.Core.Classes;
using eSIS.Core.Entities;

namespace eSIS.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var client = new WebApiClient();
            var data = client.MakeGetRequest("");
            var foo = client.DeseralizeObject<District>(data.Result);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}