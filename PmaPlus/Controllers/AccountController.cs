using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;
using PmaPlus.Models;
using PmaPlus.Services;
using PmaPlus.Tools;
using System.Net;
using System.Net.Mail; 
namespace PmaPlus.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IPhotoManager _photoManager;
        private readonly UserServices _userServices;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAuthenticationManager authenticationManager, IPhotoManager photoManager, UserServices userServices)
        {
            _userManager= userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
            _photoManager = photoManager;
            _userServices = userServices;
        }

        public ApplicationSignInManager SignInManager { get { return _signInManager; } }
        public ApplicationUserManager UserManager { get { return _userManager; } }
        public IAuthenticationManager AuthenticationManager { get { return _authenticationManager; } }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
               
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }



        /// GET /Account/LockScreen
        [AllowAnonymous]
        public ActionResult LockScreen(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            LoginViewModel model = new LoginViewModel();
            model.Email = User.Identity.Name;

            var clubAdmin = _userServices.GetClubByUserName(User.Identity.Name);
            if (clubAdmin != null)
            {
                var club = clubAdmin;
                model.Logo =  _photoManager.FileExistInStorage(FileStorageTypes.Clubs, club.Logo,club.Id) ? "../api/file/Clubs/"+club.Logo+"/"+club.Id : "../Images/Default-logo.png";
                model.Background = _photoManager.FileExistInStorage(FileStorageTypes.Clubs, club.Background, club.Id) ? "../api/file/Clubs/" + club.Background + "/" + club.Id : "../Images/Default_background.png";

                if (String.IsNullOrWhiteSpace(club.ColorTheme) || String.IsNullOrEmpty(club.ColorTheme))
                {
                    model.HexColor = "#3276b1";
                }
                else
                {
                    model.HexColor = club.ColorTheme;
                }

            }
            else
            {
                model.Logo = "../Images/Default-logo.png";
                model.Background = "../Images/Default_background.png";
                    //model.Background
                model.HexColor = "#3276b1";

            }

           
           
            AuthenticationManager.SignOut();
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
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
                                   
            if (!ModelState.IsValid)
            {
                if (String.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("loginValid", "Please enter a password");
                    return View(model);
                }

                ModelState.AddModelError("loginValid", "Username or password not recognised");
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
                            return RedirectToAction("Index", "Home");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
              
                default:
                    
                    ModelState.AddModelError("loginValid", "Username or password not recognised");
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
                            return RedirectToAction("Index", "Home");
                        }
                        return RedirectToAction("Index", "Home");
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
            AuthenticationManager.SignOut();
            return View();
        }
        public ActionResult Contact()
        {
            
            return View();
        }
        [HttpPost]
        
        public async Task<ActionResult> Contact(ContactModels c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    msg.To.Add("denys5dev@gmail.com");
                    msg.Subject = "Contact Us";
                    msg.Body = "First Name: " + c.FirstName;
                    msg.Body += "Last Name: " + c.LastName;
                    msg.Body += "Email: " + c.Email;
                    msg.Body += "Comments: " + c.Comment;
                    msg.IsBodyHtml = false;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false; // 
                    smtp.Credentials = new NetworkCredential("denys5dev@gmail.com", "password");
                    smtp.Host = "smtp.gmail.com";
                    smtp.Send(msg);
                    msg.Dispose();

                    return View("Success");
                }
                catch (Exception)
                {
                    return View("Error");
                }

            }
            return View();

        }
        
    }
}