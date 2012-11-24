using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
namespace UnitTests
{
    public class HomeController:System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        [Authorize]
        public ActionResult Logout()
        {
            return RedirectToAction("~/");
        }


        internal ActionResult ChangePassword()
        {
            throw new NotImplementedException();
        }
    }

}
