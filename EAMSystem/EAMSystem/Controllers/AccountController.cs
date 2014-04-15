using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EAMSystem.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return View("AuthError");
            }
        }

        //
        // GET: /Account/Login

        public ActionResult Login(string username, string password)
        {
            if (Membership.ValidateUser(username, password))
            {
                Membership.GetUser(username).LastLoginDate = DateTime.UtcNow;
                FormsAuthentication.SetAuthCookie(username, false);
                return RedirectToAction("Index", "Management");
            }
            else
            {
                TempData["message"] = "账号与密码不匹配";
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(string userName, string email, string password, string confirmPassword)
        {
            if (this.ValidateRegistration(userName, email, password, confirmPassword))
            {
                MembershipCreateStatus status;
                Membership.CreateUser(userName, password, email, null, null, true, out status);
                if (status == MembershipCreateStatus.Success)
                {
                    Roles.AddUserToRole(userName, "User");

                    FormsAuthentication.SetAuthCookie(userName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("_FORM", this.ErrorCodeToString(status));
                }
            }

            return View();
        }

        private string ErrorCodeToString(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again." +
                    "If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, " +
                    "please contact your system administrator.";
            }
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(userName)) ModelState.AddModelError("username", "You must specify a username.");

            if (string.IsNullOrEmpty(email)) ModelState.AddModelError("email", "You must specify an email address.");

            if (string.IsNullOrEmpty(password)) ModelState.AddModelError("password",
                string.Format("You must specify a password of {0} or more characters.", Membership.MinRequiredPasswordLength));

            if (!string.Equals(password, confirmPassword)) ModelState.AddModelError("confirmpassword", "The new password and confirmpassword do not match");

            return ModelState.IsValid;
        }
    }
}
