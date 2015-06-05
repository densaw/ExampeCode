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
            ViewBag.them = "#eb3bed";
            //TODO: Redirect roles 
            if(User.IsInRole(Role.SystemAdmin.ToString()))
                return RedirectToAction("Dashboard", "Home", new { area = "SysAdmin" });
            if (User.IsInRole(Role.ClubAdmin.ToString()))
                return RedirectToAction("Dashboard", "Home", new { area = "ClubAdmin" });
            if (User.IsInRole(Role.HeadOfAcademies.ToString()))
                return RedirectToAction("Dashboard", "Home", new { area = "HeadOfAcademy" });
            if (User.IsInRole(Role.HeadOfEducation.ToString()))
                return RedirectToAction("Dashboard", "Home", new { area = "HeadOfEducation" });
            if (User.IsInRole(Role.Physiotherapist.ToString()))
                return RedirectToAction("Dashboard", "Home", new { area = "Physio" });
            if (User.IsInRole(Role.Coach.ToString()))
                return RedirectToAction("Dashboard", "Home", new { area = "Coach" });
            if (User.IsInRole(Role.Player.ToString()))
                return RedirectToAction("Dashboard", "Home", new { area = "Player" });
            if (User.IsInRole(Role.Scout.ToString()))
                return RedirectToAction("Dashboard", "Home", new { area = "Scouts" });
            if (User.IsInRole(Role.SportsScientist.ToString()))
                return RedirectToAction("Dashboard", "Home", new { area = "SportsScientist" });
            if (User.IsInRole(Role.WelfareOfficer.ToString()))
                return RedirectToAction("Dashboard", "Home", new { area = "WelfareOfficer" });
            return View();
        }
    }
}