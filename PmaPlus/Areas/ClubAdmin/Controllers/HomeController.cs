using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PmaPlus.Services;
using PmaPlus.Tools;

namespace PmaPlus.Areas.ClubAdmin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ClubServices _clubServices;
        private readonly IPhotoManager _photoManager;
        private readonly UserServices _userServices;

        public HomeController(UserServices userServices, IPhotoManager photoManager, ClubServices clubServices)
        {
            _userServices = userServices;
            _photoManager = photoManager;
            _clubServices = clubServices;
        }



        // GET: ClubAdmin/Home
        public ActionResult Dashboard()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult Alternatives()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult Attributes()
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
        public ActionResult ClubProfile()
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
        public ActionResult Documents()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult FoodTypes()
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
        public ActionResult MatchReports()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult Players()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        public ActionResult Recipes()
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
        public ActionResult TrainingTeam()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }

        public ActionResult ProfilePage()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }

        public ActionResult CurrDetails()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            ViewBag.them = club != null ? club.ColorTheme : "#3276b1";
            return View();
        }
        
    }
}