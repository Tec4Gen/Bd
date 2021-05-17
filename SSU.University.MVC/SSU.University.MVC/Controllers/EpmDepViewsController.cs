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
    public class EpmDepViewsController : Controller
    {
        private UniversityEntities db = new UniversityEntities();

        // GET: EpmDepViews
        public ActionResult Index()
        {
            return View(db.EpmDepViews.ToList());
        }

        // GET: EpmDepViews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EpmDepView epmDepView = db.EpmDepViews.Find(id);
            if (epmDepView == null)
            {
                return HttpNotFound();
            }
            return View(epmDepView);
        }

        // GET: EpmDepViews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EpmDepViews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Group,Evalution,FirstName,LastName,MiddleName")] EpmDepView epmDepView)
        {
            if (ModelState.IsValid)
            {
                db.EpmDepViews.Add(epmDepView);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(epmDepView);
        }

        // GET: EpmDepViews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EpmDepView epmDepView = db.EpmDepViews.Find(id);
            if (epmDepView == null)
            {
                return HttpNotFound();
            }
            return View(epmDepView);
        }

        // POST: EpmDepViews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Group,Evalution,FirstName,LastName,MiddleName")] EpmDepView epmDepView)
        {
            if (ModelState.IsValid)
            {
                db.Entry(epmDepView).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(epmDepView);
        }

        // GET: EpmDepViews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EpmDepView epmDepView = db.EpmDepViews.Find(id);
            if (epmDepView == null)
            {
                return HttpNotFound();
            }
            return View(epmDepView);
        }

        // POST: EpmDepViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EpmDepView epmDepView = db.EpmDepViews.Find(id);
            db.EpmDepViews.Remove(epmDepView);
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
