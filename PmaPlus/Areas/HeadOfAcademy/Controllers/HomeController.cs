using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PmaPlus.Areas.HeadOfAcademy.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: HeadOfAcademy/Home
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}