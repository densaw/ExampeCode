using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PmaPlus.Areas.ClubAdmin.Controllers
{
    public class HomeController : Controller
    {
        // GET: ClubAdmin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}