﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PmaPlus.Areas.SysAdmin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: SysAdmin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}