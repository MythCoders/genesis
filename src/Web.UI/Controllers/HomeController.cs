using System.Collections.Generic;
using System.Web.Mvc;
using eSIS.Core.Classes;
using eSIS.Core.Entities;
using eSIS.Core.UI;

namespace eSIS.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var url = new UrlBuilder()
                .SubDirectory("District")
                .Method("GetAll")
                .Generate();

            var completeUrl = url.GeneratePath();

            var client = new WebApiClient();
            var data = client.MakeGetRequest(completeUrl);
            var allDistricts = client.DeseralizeObject<List<District>>(data.Result).Result;

            return View(allDistricts);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}