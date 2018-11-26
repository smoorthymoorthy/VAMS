using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VAMS.Common;

namespace VAMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            if (SessionHelper.Current.UserID == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }




    }
}