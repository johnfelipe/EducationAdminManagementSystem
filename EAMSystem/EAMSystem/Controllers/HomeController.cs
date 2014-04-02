using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EAMSystem.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //Roles.CreateRole("Student");
            //Roles.CreateRole("Teacher");
            //Roles.CreateRole("Admin");

            //Membership.CreateUser("admin", "123456");

            ViewData["width"] = 325;
            ViewData["height"] = 300;

            return View();
        }

    }
}
