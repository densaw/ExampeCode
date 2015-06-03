using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PmaPlus.Services;

namespace PmaPlus.Areas.Player.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserServices _userServices;

        public HomeController(UserServices userServices)
        {
            _userServices = userServices;
        }
        // GET: Player/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}