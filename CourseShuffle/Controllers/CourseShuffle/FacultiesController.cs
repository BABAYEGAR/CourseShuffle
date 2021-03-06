﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Objects.Entities;
using CourseShuffle.Data.Service.Enum;

namespace CourseShuffle.Controllers.CourseShuffle
{
    public class FacultiesController : Controller
    {
        private readonly FacultyDataContext _db = new FacultyDataContext();
        private readonly DepartmentDataContext _dbc = new DepartmentDataContext();

        // GET: Faculties
        public ActionResult Index()
        {
            return View(_db.Faculties.ToList());
        }

        // GET: Faculties/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var faculty = _db.Faculties.Find(id);
            if (faculty == null)
                return HttpNotFound();
            return View(faculty);
        }

        // GET: Faculties/Create
        public ActionResult Create(long? id)
        {
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FacultyId,Name,Description")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
                if (loggedinuser != null)
                {
                    faculty.DateCreated = DateTime.Now;
                    faculty.DateLastModified = DateTime.Now;
                    faculty.CreatedBy = loggedinuser.AppUserId;
                    faculty.LastModifiedBy = loggedinuser.AppUserId;
                    _db.Faculties.Add(faculty);
                    _db.SaveChanges();
                    TempData["faculty"] = "A new faculty has been created successfully!";
                    TempData["notificationtype"] = NotificationType.Success.ToString();
                    return RedirectToAction("Index");
                }
                TempData["faculty"] = "Your session has expired,Login Again!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
            }

            return View(faculty);
        }

        // GET: Faculties/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var faculty = _db.Faculties.Find(id);
            if (faculty == null)
                return HttpNotFound();
            return View(faculty);
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FacultyId,Name,Description")] Faculty faculty,
            FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
                if (loggedinuser != null)
                {
                    faculty.DateCreated = Convert.ToDateTime(collectedValues["DateCreated"]);
                    faculty.DateLastModified = DateTime.Now;
                    faculty.LastModifiedBy = loggedinuser.AppUserId;
                    _db.Entry(faculty).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                TempData["faculty"] = "Your session has expired,Login Again!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
            }
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var faculty = _db.Faculties.Find(id);
            if (faculty == null)
                return HttpNotFound();
            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var faculty = _db.Faculties.Find(id);
            _db.Faculties.Remove(faculty);
            _db.SaveChanges();
            TempData["faculty"] = "A faculty has been deleted successfully!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}