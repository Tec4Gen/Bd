using SSU.University.MVC.Models;
using SSU.University.MVC.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SSU.University.MVC.Controllers
{
    public class HomeController : Controller
    {
        private UniversityEntities db = new UniversityEntities();

        public ActionResult Index()
        {


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.User model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(p=> p.Login == model.Login))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, createPersistentCookie: true);
                    if (returnUrl != null)
                    {
                        return Redirect("Login");
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Bad login/password");
                    return View(model);
                }
            }
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult LoginBlock()
        {
            if (User.Identity.IsAuthenticated)
            {
                return PartialView("_LogoutPartial");
            }
            else
            {
                return PartialView("_LoginPartial");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}