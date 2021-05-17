using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSU.University.MVC.Models;

namespace SSU.University.MVC.Controllers
{
    public class EducationPlansController : Controller
    {
        private UniversityEntities db = new UniversityEntities();

        // GET: EducationPlans
        public ActionResult Index()
        {
            var educationPlans = db.EducationPlans.Include(e => e.Discipline).Include(e => e.Specialty);
            return View(educationPlans.ToList());
        }

        // GET: EducationPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationPlan educationPlan = db.EducationPlans.Find(id);
            if (educationPlan == null)
            {
                return HttpNotFound();
            }
            return View(educationPlan);
        }

        // GET: EducationPlans/Create
        public ActionResult Create()
        {
            ViewBag.IdDiscipline = new SelectList(db.Disciplines, "Id", "Title");
            ViewBag.IdSpecialty = new SelectList(db.Specialties, "Id", "Title");
            return View();
        }

        // POST: EducationPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdSpecialty,IdDiscipline,TotalTime,DateInstall")] EducationPlan educationPlan)
        {
            if (ModelState.IsValid)
            {
                db.EducationPlans.Add(educationPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDiscipline = new SelectList(db.Disciplines, "Id", "Title", educationPlan.IdDiscipline);
            ViewBag.IdSpecialty = new SelectList(db.Specialties, "Id", "Title", educationPlan.IdSpecialty);
            return View(educationPlan);
        }

        // GET: EducationPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationPlan educationPlan = db.EducationPlans.Find(id);
            if (educationPlan == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDiscipline = new SelectList(db.Disciplines, "Id", "Title", educationPlan.IdDiscipline);
            ViewBag.IdSpecialty = new SelectList(db.Specialties, "Id", "Title", educationPlan.IdSpecialty);
            return View(educationPlan);
        }

        // POST: EducationPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdSpecialty,IdDiscipline,TotalTime,DateInstall")] EducationPlan educationPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(educationPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDiscipline = new SelectList(db.Disciplines, "Id", "Title", educationPlan.IdDiscipline);
            ViewBag.IdSpecialty = new SelectList(db.Specialties, "Id", "Title", educationPlan.IdSpecialty);
            return View(educationPlan);
        }

        // GET: EducationPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationPlan educationPlan = db.EducationPlans.Find(id);
            if (educationPlan == null)
            {
                return HttpNotFound();
            }
            return View(educationPlan);
        }

        // POST: EducationPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EducationPlan educationPlan = db.EducationPlans.Find(id);
            db.EducationPlans.Remove(educationPlan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
