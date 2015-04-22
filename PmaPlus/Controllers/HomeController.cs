using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PmaPlus.Model;

namespace PmaPlus.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //TODO: Redirect roles 
            if(User.IsInRole(Role.SystemAdmin.ToString()))
                return RedirectToAction("Dashbord", "Home", new { area = "SysAdmin" });
            return View();
        }
    }
}