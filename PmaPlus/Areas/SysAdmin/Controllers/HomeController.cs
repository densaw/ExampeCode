using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PmaPlus.Areas.SysAdmin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Dashbord()
        {
            return View();
        }

        public ActionResult FaCourses()
        {
            return PartialView("FaCourses");
        }
    }
}