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
    public class SuspendedStatesController : Controller
    {
        private BITCollege_RSContext db = new BITCollege_RSContext();

        // GET: SuspendedStates
        public ActionResult Index()
        {
            return View(SuspendedState.GetInstance());
        }

        // GET: SuspendedStates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendedState suspendedState = db.SuspendedStates.Find(id);
            if (suspendedState == null)
            {
                return HttpNotFound();
            }
            return View(suspendedState);
        }

        // GET: SuspendedStates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuspendedStates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GradePointStateId,LowerLimit,UpperLimit,TuitionRateFactor")] SuspendedState suspendedState)
        {
            if (ModelState.IsValid)
            {
                db.GradePointStates.Add(suspendedState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(suspendedState);
        }

        // GET: SuspendedStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendedState suspendedState = db.SuspendedStates.Find(id);
            if (suspendedState == null)
            {
                return HttpNotFound();
            }
            return View(suspendedState);
        }

        // POST: SuspendedStates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GradePointStateId,LowerLimit,UpperLimit,TuitionRateFactor")] SuspendedState suspendedState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suspendedState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suspendedState);
        }

        // GET: SuspendedStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendedState suspendedState = db.SuspendedStates.Find(id);
            if (suspendedState == null)
            {
                return HttpNotFound();
            }
            return View(suspendedState);
        }

        // POST: SuspendedStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuspendedState suspendedState = db.SuspendedStates.Find(id);
            db.GradePointStates.Remove(suspendedState);
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
