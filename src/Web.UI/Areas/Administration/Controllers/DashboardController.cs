using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eSIS.Web.UI.Areas.Administration.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Administration/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}