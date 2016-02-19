using System.Web.Mvc;

namespace MC.eSIS.Web.UI.Areas.Administration.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Administration/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Config()
        {
            return View();
        }
    }
}