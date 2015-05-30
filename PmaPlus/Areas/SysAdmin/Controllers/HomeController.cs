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
        public ActionResult Dashboard()
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

       

        public ActionResult SkillsKnowledge()
        {
            return View();
        }

        public ActionResult NutritionAlternatives()
        {
            
            return View();
        }
        public ActionResult NutritionFoodType()
        {
            return View();
        }
        public ActionResult NutritionRecipes()
        {
            return View();
        }
        public ActionResult PhysiotherapyBodyParts()
        {
            return View();
        }
        public ActionResult PhysiotherapyExercise()
        {
            return View();
        }
        public ActionResult Reports()
        {
            return View();
        }
        public ActionResult Scenarios()
        {
            return View();
        }
        public ActionResult SiteSettingsLogin()
        {
            return View();
        }
        public ActionResult SportsScienceExercises()
        {
            return View();
        }
        public ActionResult SportsScienceTests()
        {
            return View();
        }

        public ActionResult Targets()
        {
            return View();
        }

        public ActionResult LevelSkills()
        {
            return View();
        }

        public ActionResult Exercise()
        {           
            return View();
        }
        public ActionResult NutritionNews()
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