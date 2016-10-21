﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BhuInfo.Data.Service.Encryption;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Factory.FactoryData;
using CourseShuffle.Data.Objects.Entities;
using CourseShuffle.Data.Service.Encryption;
using CourseShuffle.Data.Service.Enum;

namespace CourseShuffle.Controllers.CourseShuffle
{
    public class CoursesController : Controller
    {
        private readonly CourseDataContext _db = new CourseDataContext();

        // GET: Courses
        public ActionResult Index()
        {
            var courses = _db.Courses.Include(c => c.Department).Include(c => c.Level);
            return View(courses.ToList());
        }
        public ActionResult ViewCoursesForDepartment(long id)
        {
            var courses = new CourseFactory().GetAllCoursesForADepartment(id);
            return View("DeparmentCourses", courses);
        }
        public ActionResult ViewCoursesForLevel(long levelId,long departmentId)
        {
            var courses = new CourseFactory().GetAllCoursesForALevel(levelId,departmentId);
            return View("LevelCorses", courses);
        }

        // GET: Courses/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = _db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        // GET: Courses/Create
        public ActionResult Create(long? id)
        {
            if (id <= 0)
            {
                id = 0;
            }
            ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
            ViewBag.Level = id ?? 0;
            ViewBag.LevelId = new SelectList(_db.Levels, "LevelId", "Name");
            ViewBag.AppUserId = new SelectList(_db.AppUsers.Where(n=>n.Role == UserType.Lecturer.ToString()), "AppUserId", "DisplayName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CoursesId,CourseName,CourseCode,CreditUnit,AppUserId,DepartmentId")] Courses courses,FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                var levelId = Convert.ToInt64(collectedValues["levelId"]);
                if (collectedValues["levelId"] == null)
                {
                    levelId = Convert.ToInt64(collectedValues["Level"]);
                }

                courses.LevelId = levelId;
              
                 var departmentId = Convert.ToInt64(collectedValues["departmentId"]);
                courses.Semester = typeof(SemesterType).GetEnumName(int.Parse(collectedValues["Semester"]));
                courses.DateCreated  = DateTime.Now;
                courses.DateLastModified = DateTime.Now;
                courses.LastModifiedBy = 1;
                courses.CreatedBy = 1;
                _db.Courses.Add(courses);
                _db.SaveChanges();
                return RedirectToAction("ViewCoursesForLevel",new {levelId, departmentId });
            }

            ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name", courses.DepartmentId);
            ViewBag.LevelId = new SelectList(_db.Levels, "LevelId", "Name", courses.LevelId);
            ViewBag.AppUserId = new SelectList(_db.AppUsers.Where(n => n.Role == UserType.Lecturer.ToString()), "AppUserId", "DisplayName");
            return View(courses);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = _db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CoursesId,CourseName,CourseCode,CreditUnit,AppUserId,LevelId,DepartmentId,CreatedBy")] Courses courses,FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                courses.DateLastModified = DateTime.Now;
                courses.LastModifiedBy = 1;
                courses.DateCreated =Convert.ToDateTime(collectedValues["DateCreated"]);
                _db.Entry(courses).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name", courses.DepartmentId);
            //ViewBag.LevelId = new SelectList(_db.Levels, "LevelId", "Name", courses.LevelId);
            return View(courses);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = _db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Courses courses = _db.Courses.Find(id);
            _db.Courses.Remove(courses);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
