﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FishToolsStoreECommerceApp.Areas.ManagerPanel.Filters;
using FishToolsStoreECommerceApp.Models;

namespace FishToolsStoreECommerceApp.Areas.ManagerPanel.Controllers
{
    [ManagerAuthorizationFilter]
    public class CategoryController : Controller
    {
        private FishToolsStoreModel db = new FishToolsStoreModel();

        // GET: ManagerPanel/Category
        public ActionResult Index()
        {
            return View(db.Categories.Where(b => b.IsDeleted == false).ToList());
        }
        public ActionResult AllIndex()
        {
            return View(db.Categories.ToList());
        }

        // GET: ManagerPanel/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: ManagerPanel/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagerPanel/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: ManagerPanel/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Category");
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            return View(category);
        }

        // POST: ManagerPanel/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,IsActive,IsDeleted")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: ManagerPanel/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Category");
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            return View(category);
        }
        public ActionResult ReDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Category");
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            category.IsDeleted = false;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // POST: ManagerPanel/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            category.IsDeleted = true;
            category.IsActive = false;
            db.Entry(category).State = EntityState.Modified;
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
