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
    public class PerfomancesController : Controller
    {
        private UniversityEntities db = new UniversityEntities();

        // GET: Perfomances
        public ActionResult Index()
        {
            var perfomances = db.Perfomances.Include(p => p.Discipline).Include(p => p.Student);
            return View(perfomances.ToList());
        }

        // GET: Perfomances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfomance perfomance = db.Perfomances.Find(id);
            if (perfomance == null)
            {
                return HttpNotFound();
            }
            return View(perfomance);
        }

        // GET: Perfomances/Create
        public ActionResult Create()
        {
            ViewBag.IdDiscipline = new SelectList(db.Disciplines, "Id", "Title");
            ViewBag.IdStudent = new SelectList(db.Students, "Id", "FirstName");
            return View();
        }

        // POST: Perfomances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdStudent,IdDiscipline,Evalution,DateOfDelivery")] Perfomance perfomance)
        {
            if (ModelState.IsValid)
            {
                db.Perfomances.Add(perfomance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDiscipline = new SelectList(db.Disciplines, "Id", "Title", perfomance.IdDiscipline);
            ViewBag.IdStudent = new SelectList(db.Students, "Id", "FirstName", perfomance.IdStudent);
            return View(perfomance);
        }

        // GET: Perfomances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfomance perfomance = db.Perfomances.Find(id);
            if (perfomance == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDiscipline = new SelectList(db.Disciplines, "Id", "Title", perfomance.IdDiscipline);
            ViewBag.IdStudent = new SelectList(db.Students, "Id", "FirstName", perfomance.IdStudent);
            return View(perfomance);
        }

        // POST: Perfomances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdStudent,IdDiscipline,Evalution,DateOfDelivery")] Perfomance perfomance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perfomance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDiscipline = new SelectList(db.Disciplines, "Id", "Title", perfomance.IdDiscipline);
            ViewBag.IdStudent = new SelectList(db.Students, "Id", "FirstName", perfomance.IdStudent);
            return View(perfomance);
        }

        // GET: Perfomances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfomance perfomance = db.Perfomances.Find(id);
            if (perfomance == null)
            {
                return HttpNotFound();
            }
            return View(perfomance);
        }

        // POST: Perfomances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Perfomance perfomance = db.Perfomances.Find(id);
            db.Perfomances.Remove(perfomance);
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
