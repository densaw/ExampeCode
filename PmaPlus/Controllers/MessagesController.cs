using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PmaPlus.Data;
using PmaPlus.Model.Models;

namespace PmaPlus.Controllers
{
    public class MessagesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
