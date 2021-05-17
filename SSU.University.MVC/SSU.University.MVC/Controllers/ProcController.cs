using SSU.University.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSU.University.MVC.Controllers
{
    public class ProcController : Controller
    {
        private UniversityEntities db = new UniversityEntities();
        // GET: Proc
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllStudnet()
        {
            return View(db.GetAllStudnet());
        }
        [HttpPost]
        public ActionResult StudentsByDate(DateTime date)
        {
            return View(db.GetStudentBydateReceipt(date));
        }

        [HttpGet]
        public ActionResult StudentsByDate()
        {
            return View();
        }

    }
}