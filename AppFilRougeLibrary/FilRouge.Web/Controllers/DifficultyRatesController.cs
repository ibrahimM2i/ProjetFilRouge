﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FilRouge.Web.Entities;

namespace FilRouge.Web.Controllers
{
    public class DifficultyRatesController : Controller
    {
        private FilRougeDBContext db = new FilRougeDBContext();

        // GET: DifficultyRates
        public ActionResult Index()
        {
            var difficultyRates = db.DifficultyRates.Include(d => d.Difficulty).Include(d => d.DifficultyMaster);
            return View(difficultyRates.ToList());
        }

        // GET: DifficultyRates/Details/5
        public ActionResult Details(int? id)
        {
            return View(serviceDifficulty.GetDifficultyById(id));
        }

        // GET: DifficultyRates/Create
        public ActionResult Create()
        {
            ViewBag.DifficultyId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName");
            ViewBag.DifficultyMasterId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName");
            return View();
        }

        // POST: DifficultyRates/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DifficultyMasterId,DifficultyId,Rate")] DifficultyRate difficultyRate)
        {
            if (ModelState.IsValid)
            {
                db.DifficultyRates.Add(difficultyRate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DifficultyId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName", difficultyRate.DifficultyId);
            ViewBag.DifficultyMasterId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName", difficultyRate.DifficultyMasterId);
            return View(difficultyRate);
        }

        // GET: DifficultyRates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DifficultyRate difficultyRate = db.DifficultyRates.Find(id);
            if (difficultyRate == null)
            {
                return HttpNotFound();
            }
            ViewBag.DifficultyId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName", difficultyRate.DifficultyId);
            ViewBag.DifficultyMasterId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName", difficultyRate.DifficultyMasterId);
            return View(difficultyRate);
        }

        // POST: DifficultyRates/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DifficultyMasterId,DifficultyId,Rate")] DifficultyRate difficultyRate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(difficultyRate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DifficultyId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName", difficultyRate.DifficultyId);
            ViewBag.DifficultyMasterId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName", difficultyRate.DifficultyMasterId);
            return View(difficultyRate);
        }

        // GET: DifficultyRates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DifficultyRate difficultyRate = db.DifficultyRates.Find(id);
            if (difficultyRate == null)
            {
                return HttpNotFound();
            }
            return View(difficultyRate);
        }

        // POST: DifficultyRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DifficultyRate difficultyRate = db.DifficultyRates.Find(id);
            db.DifficultyRates.Remove(difficultyRate);
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