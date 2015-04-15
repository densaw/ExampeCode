using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PmaPlus.Model;
using PmaPlus.Models;

namespace PmaPlus.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User() {UserName = model.Email, Email = model.Email};
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                    
                }
            }
            return View();
        }


        
        /// GET /Account/LockScreen
        [AllowAnonymous]
        public ActionResult LockScreen(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            LoginViewModel model = new LoginViewModel();
            model.Email = User.Identity.Name;
            SignInManager.AuthenticationManager.SignOut();
            Response.Cookies.Clear();
            Session.Clear();
            Session.Abandon();
            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("loginValid", "Email address or password");
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            
            switch (result)
            {
                case SignInStatus.Success:
                    {
                        if (model.Email.ToLower() == "systemadmin@local.com")
                        {
                            //ViewBag.JSAlert = "toastr.success('Without any options', 'Simple notification!')";
                            return RedirectToAction("Index", "Home", new { area = "SysAdmin" });
                        }
                        return RedirectToAction("Index", "Home", new { area = "SysAdmin" });
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("loginValid", "Email address or password");
                    return View(model);
            }

        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LockScreen(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("PassValid", "Please enter password");
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    {

                        if (model.Email.ToLower() == "systemadmin@local.com")
                        {
                            //ViewBag.JSAlert = "toastr.success('Without any options', 'Simple notification!')";
                            return RedirectToAction("Index", "Home", new { area = "SysAdmin" });
                        }
                        return RedirectToAction("Index", "Home", new { area = "SysAdmin" });
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("PassValid", "Sorry that password is incorrect, please try again");
                    return View(model);
            }

        }


        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null /* || !(await UserManager.IsEmailConfirmedAsync(user.Id))*/)
                {
                    ModelState.AddModelError("emailValid", "Your email has not been found please contact the club if you can’t remember.");
                    // Don't reveal that the user does not exist or is not confirmed
                    return View(model);
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                TempData["toastrCall"] = true;
                return RedirectToAction("Login", "Account");
            }
            ModelState.AddModelError("emailValid", "Please enter correct email.");

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOff()
        {
            SignInManager.AuthenticationManager.SignOut();
            return View();
        }


    }
}