using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PmaPlus.Model;
using PmaPlus.Services;

namespace PmaPlus.Areas.Coach.Controllers
{
    [Authorize(Roles = "Coach")]
    public class HomeController : Controller
    {
        private readonly UserServices _userServices;

        public HomeController(UserServices userServices)
        {
            _userServices = userServices;
        }
        
    
        // GET: Coach/Home
        public ActionResult Dashboard()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult PlayersTable()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult Wizzard()
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
        public ActionResult Documents()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult Nutrition()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult Fitness()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult Tracker()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult Wellbeing()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult MatchReports()
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
        public ActionResult Reports()
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
        public ActionResult SkillVideos()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult SkillTestRequests()
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
        public ActionResult TrainingTeam()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult WellbeingRPETests()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult Communications()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }

    }
}