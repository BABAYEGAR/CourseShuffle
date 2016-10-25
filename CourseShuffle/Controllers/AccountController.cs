using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CourseShuffle.Data.Factory.FactoryData;
using CourseShuffle.Data.Objects.Entities;
using CourseShuffle.Data.Service.Encryption;
using CourseShuffle.Data.Service.Enum;
using CourseShuffle.Models;
using Microsoft.AspNet.Identity.Owin;

namespace CourseShuffle.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult ProfileDetails(string Id)
        {
            var userId = Convert.ToInt64(Id);
            var user = new AppUserFactory().GetAppUserById((int)userId);
            Session["viewprofilebyotheruser"] = user;
            return View("ProfileDetails", user);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl, FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                var appUser = new AuthenticationFactory().AuthenticateAppUserLogin(collectedValues["Email"].Trim(),
                    collectedValues["Password"].Trim());
                if (appUser != null)
                {
                    if( appUser.Role == UserType.Administrator.ToString())
                    {
                        Session["courseshuffleloggedinuser"] = appUser;
                        return RedirectToAction("Index", "AppUsers", model);
                    }
                    if (appUser.Role == UserType.Lecturer.ToString())
                    {
                        Session["courseshuffleloggedinuser"] = appUser;
                        return RedirectToAction("Index", "Departments", model);
                    }
                }
            }
            return View();
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
        public ActionResult ForgotPassword(FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                new AuthenticationFactory().ForgotPasswordRequest(collectedValues["Email"].Trim());
                return RedirectToAction("Login");
            }
            return View();
        }

        //
        // GET: /Account/ChangePassword
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            return View("ChangePassword");
        }

        /// <summary>
        ///     This controller method changes the user password
        /// </summary>
        /// <param name="collectedValues"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(FormCollection collectedValues)
        {
            var oldPassword = collectedValues["OldPassword"];
            var newPassword = collectedValues["NewPassword"];
            var confirmPassword = collectedValues["ConfirmNewPassword"];
            var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
            if (ModelState.IsValid)
                if (newPassword == confirmPassword)
                {
                    var hashPassword = new Md5Ecryption().ConvertStringToMd5Hash(oldPassword.Trim());
                    if ((loggedinuser != null) && (hashPassword == loggedinuser.Password))
                    {
                        if (new AuthenticationFactory().ChangeUserPassword(Convert.ToInt64(loggedinuser.AppUserId),
                            oldPassword,
                            newPassword))
                        {
                            TempData["password"] = "Your Pasword has been changed successfully,Please Login Again!";
                            TempData["notificationtype"] = NotificationType.Success.ToString();
                            Session["courseshuffleloggedinuser"] = null;
                            return RedirectToAction("Login", "Account");
                        }
                    }
                    else
                    {
                        TempData["password"] = "Wrong password,Try Again!";
                        TempData["notificationtype"] = NotificationType.Danger.ToString();
                        return View("ChangePassword");
                    }
                }
                else
                {
                    TempData["password"] = "Make sure your the password and confirm password are the same!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return View("ChangePassword");
                }
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(int Id)
        {
            var user = new AppUserFactory().GetAppUserById(Id);
            return View(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(FormCollection collectedValues)
        {
            var userId = Convert.ToInt32(collectedValues["Id"]);
            var password = collectedValues["Password"];
            var confirmPassword = collectedValues["confirmPassword"];
            if (password == confirmPassword)
            {
                new AuthenticationFactory().ResetUserPassword(collectedValues["confirmPassword"], userId);
            }
            else
            {
                TempData["password"] = "Make sure your the password and confirm password are the same!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("ResetPassword", "Account", new {Id = userId});
            }
            return View("Login");
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["courseshuffleloggedinuser"] = null;
            FormsAuthentication.RedirectToLoginPage(null);
            return RedirectToAction("Login", "Account");
        }
    }
}