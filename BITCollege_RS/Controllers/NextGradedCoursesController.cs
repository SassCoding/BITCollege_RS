﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BITCollege_RS.Data;
using BITCollege_RS.Models;

namespace BITCollege_RS.Controllers
{
    public class NextGradedCoursesController : Controller
    {
        private BITCollege_RSContext db = new BITCollege_RSContext();

        // GET: NextGradedCourses
        public ActionResult Index()
        {
            return View(NextGradedCourse.GetInstance());
        }

        // GET: NextGradedCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextGradedCourse nextGradedCourse = db.NextGradedCourses.Find(id);
            if (nextGradedCourse == null)
            {
                return HttpNotFound();
            }
            return View(nextGradedCourse);
        }

        // GET: NextGradedCourses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NextGradedCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NextUniqueNumberId,NextAvailableNumber")] NextGradedCourse nextGradedCourse)
        {
            if (ModelState.IsValid)
            {
                db.NextGradedCourses.Add(nextGradedCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextGradedCourse);
        }

        // GET: NextGradedCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextGradedCourse nextGradedCourse = db.NextGradedCourses.Find(id);
            if (nextGradedCourse == null)
            {
                return HttpNotFound();
            }
            return View(nextGradedCourse);
        }

        // POST: NextGradedCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NextUniqueNumberId,NextAvailableNumber")] NextGradedCourse nextGradedCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextGradedCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextGradedCourse);
        }

        // GET: NextGradedCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextGradedCourse nextGradedCourse = db.NextGradedCourses.Find(id);
            if (nextGradedCourse == null)
            {
                return HttpNotFound();
            }
            return View(nextGradedCourse);
        }

        // POST: NextGradedCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NextGradedCourse nextGradedCourse = db.NextGradedCourses.Find(id);
            db.NextGradedCourses.Remove(nextGradedCourse);
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
