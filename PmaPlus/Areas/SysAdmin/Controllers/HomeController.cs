using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PmaPlus.Model;\

namespace PmaPlus.Areas.SysAdmin.Controllers
{
    public class HomeController : Controller
    {
        // GET: SysAdmin/Home
        public ActionResult Index()
        {
            
            return View();
        }

    }
}