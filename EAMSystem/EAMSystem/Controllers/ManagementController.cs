using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EAMSystem.Controllers
{
    public class ManagementController : Controller
    {
        //
        // GET: /Management/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Management/EditStudent (admin)

        public ActionResult EditStudent()
        {
            if(Request.IsAuthenticated && User.IsInRole("Teacher"))
            {
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Management/NewStudent (admin)

        public ActionResult NewStudent()
        {
            if (Request.IsAuthenticated && User.IsInRole("Teacher"))
            {
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }
    }
}
