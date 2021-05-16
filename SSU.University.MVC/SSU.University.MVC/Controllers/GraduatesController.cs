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
    public class GraduatesController : Controller
    {
        private UniversityEntities db = new UniversityEntities();

        // GET: Graduates
        public ActionResult Index()
        {
            var graduates = db.Graduates.Include(g => g.Student);
            return View(graduates.ToList());
        }

        // GET: Graduates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Graduates graduates = db.Graduates.Find(id);
            if (graduates == null)
            {
                return HttpNotFound();
            }
            return View(graduates);
        }

        // GET: Graduates/Create
        public ActionResult Create()
        {
            ViewBag.IdStudent = new SelectList(db.Student, "Id", "FirstName");
            return View();
        }

        // POST: Graduates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdStudent,DateGraduates")] Graduates graduates)
        {
            if (ModelState.IsValid)
            {
                db.Graduates.Add(graduates);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdStudent = new SelectList(db.Student, "Id", "FirstName", graduates.IdStudent);
            return View(graduates);
        }

        // GET: Graduates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Graduates graduates = db.Graduates.Find(id);
            if (graduates == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdStudent = new SelectList(db.Student, "Id", "FirstName", graduates.IdStudent);
            return View(graduates);
        }

        // POST: Graduates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdStudent,DateGraduates")] Graduates graduates)
        {
            if (ModelState.IsValid)
            {
                db.Entry(graduates).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdStudent = new SelectList(db.Student, "Id", "FirstName", graduates.IdStudent);
            return View(graduates);
        }

        // GET: Graduates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Graduates graduates = db.Graduates.Find(id);
            if (graduates == null)
            {
                return HttpNotFound();
            }
            return View(graduates);
        }

        // POST: Graduates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Graduates graduates = db.Graduates.Find(id);
            db.Graduates.Remove(graduates);
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
