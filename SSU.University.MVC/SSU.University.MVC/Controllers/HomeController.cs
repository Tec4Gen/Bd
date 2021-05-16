using SSU.University.MVC.Models;
using SSU.University.MVC.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [HttpPost]
        public ActionResult Login([Bind(Include = "Id,LastName")] User user)
        {
            if (ModelState.IsValid)
            {
                if (user.FirstName == "Admin" && user.LastName == "Admin")
                    return View("Admin");

                var employee = db.Employee.FirstOrDefault(c => c.FirstName == user.FirstName && c.LastName == user.LastName);
                var student = db.Student.FirstOrDefault(t => t.FirstName == user.FirstName && t.LastName == user.LastName);

                if (employee != null)
                    return RedirectToAction("Details", "Employee", new { Id = employee.Id });

                if (student != null)
                    return RedirectToAction("Details", "Student", new { Id = student.Id });
            }

            return View("Modal");
        }
    }
}