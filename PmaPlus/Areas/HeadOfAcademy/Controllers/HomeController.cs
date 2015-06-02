using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PmaPlus.Areas.HeadOfAcademy.Controllers
{
    public class HomeController : Controller
    {
        // GET: HeadOfAcademy/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}