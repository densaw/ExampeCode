﻿using System;
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
            return View();
        }
    }
}