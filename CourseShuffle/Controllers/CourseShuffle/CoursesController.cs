using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Objects.Entities;

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
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
            ViewBag.LevelId = new SelectList(_db.Levels, "LevelId", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CoursesId,CourseName,CourseCode,CreditUnit,LevelId,DepartmentId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                _db.Courses.Add(courses);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name", courses.DepartmentId);
            ViewBag.LevelId = new SelectList(_db.Levels, "LevelId", "Name", courses.LevelId);
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
            ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name", courses.DepartmentId);
            ViewBag.LevelId = new SelectList(_db.Levels, "LevelId", "Name", courses.LevelId);
            return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CoursesId,CourseName,CourseCode,CreditUnit,LevelId,DepartmentId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(courses).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name", courses.DepartmentId);
            ViewBag.LevelId = new SelectList(_db.Levels, "LevelId", "Name", courses.LevelId);
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
