using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PmaPlus.Services;

namespace PmaPlus.Areas.HeadOfAcademy.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserServices _userServices;

        public HomeController(UserServices userServices)
        {
            _userServices = userServices;
        }


        // GET: HeadOfAcademy/Home
        public ActionResult Dashboard()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            
            return View();
        }

        public ActionResult ClubDiary()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }

        public ActionResult Curriculums()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }

        public ActionResult Statements()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }

        public ActionResult Teams()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }

        public ActionResult ToDoList()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }

        public ActionResult Scenarios()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }

        public ActionResult SkillsAndKnowledge()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult Injuries()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult Documents()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult Reports()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }

        public ActionResult Profile()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
    }
}