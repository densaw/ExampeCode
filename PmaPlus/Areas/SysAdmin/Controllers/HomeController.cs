using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PmaPlus.Models;

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
            return View();
        }

        public ActionResult Clubs()
        {
            return View();
        }

        public ActionResult CurriculumTypes()
        {
            return View();
        }

        public ActionResult Empty(RouteData rd)
        {
            var routeValueDictionary = rd.Values;
            var path = string.Format("{0}/{1}", routeValueDictionary["controller"], routeValueDictionary["action"]);
            var name = routeValueDictionary["action"];
            return PartialView("Empty", new EmptyViewModel()
            {
                Breadcrumb = path,
                PageName = name.ToString()
            });
        }
    }
}