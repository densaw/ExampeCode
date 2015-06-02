using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PmaPlus.Areas.HeadOfAcademy.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: HeadOfAcademy/Home
        public ActionResult Dashboard()
        {            
            return View();
        }

        public ActionResult ClubDiary()
        {            
            return View();
        }

        public ActionResult Curriculums()
        {            
            return View();
        }

        public ActionResult Statements()
        {           
            return View();
        }

        public ActionResult Teams()
        {           
            return View();
        }

        public ActionResult ToDoList()
        {           
            return View();
        }

        public ActionResult Scenarios()
        {           
            return View();
        }

        public ActionResult SkillsAndKnowledge()
        {            
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }
    }
}