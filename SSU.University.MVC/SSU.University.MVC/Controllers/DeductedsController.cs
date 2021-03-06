﻿using System;
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
    public class DeductedsController : Controller
    {
        private UniversityEntities db = new UniversityEntities();

        // GET: Deducteds
        public ActionResult Index()
        {
            var deducteds = db.Deducteds.Include(d => d.Student);
            return View(deducteds.ToList());
        }

        // GET: Deducteds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deducted deducted = db.Deducteds.Find(id);
            if (deducted == null)
            {
                return HttpNotFound();
            }
            return View(deducted);
        }

        // GET: Deducteds/Create
        public ActionResult Create()
        {
            ViewBag.IdStudent = new SelectList(db.Students, "Id", "FirstName");
            return View();
        }

        // POST: Deducteds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdStudent,DateDeducted")] Deducted deducted)
        {
            if (ModelState.IsValid)
            {
                db.Deducteds.Add(deducted);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdStudent = new SelectList(db.Students, "Id", "FirstName", deducted.IdStudent);
            return View(deducted);
        }

        // GET: Deducteds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deducted deducted = db.Deducteds.Find(id);
            if (deducted == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdStudent = new SelectList(db.Students, "Id", "FirstName", deducted.IdStudent);
            return View(deducted);
        }

        // POST: Deducteds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdStudent,DateDeducted")] Deducted deducted)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deducted).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdStudent = new SelectList(db.Students, "Id", "FirstName", deducted.IdStudent);
            return View(deducted);
        }

        // GET: Deducteds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deducted deducted = db.Deducteds.Find(id);
            if (deducted == null)
            {
                return HttpNotFound();
            }
            return View(deducted);
        }

        // POST: Deducteds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deducted deducted = db.Deducteds.Find(id);
            db.Deducteds.Remove(deducted);
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
