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
    public class DepartmentsController : Controller
    {
        private DepartmentDataContext _db = new DepartmentDataContext();

        // GET: Departments
        public ActionResult Index()
        {
            var departments = _db.Departments.Include(d => d.Faculty);
            return View(departments.ToList());
        }

        // GET: Departments/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.FacultyId = new SelectList(_db.Faculties, "FacultyId", "Name");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,Name,Description")] Department department,FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                department.FacultyId = Convert.ToInt64(collectedValues["FacultyId"]);
                department.DateCreated = DateTime.Now;
                department.DateLastModified = DateTime.Now;
                department.CreatedBy = 1;
                department.LastModifiedBy = 1;
                _db.Departments.Add(department);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FacultyId = new SelectList(_db.Faculties, "FacultyId", "Name", department.FacultyId);
            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacultyId = new SelectList(_db.Faculties, "FacultyId", "Name", department.FacultyId);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentId,Name,Description,FacultyId,CreatedBy")] Department department,FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                department.DateCreated = Convert.ToDateTime(collectedValues["DateCreated"]);
                department.DateLastModified = DateTime.Now;
                _db.Entry(department).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FacultyId = new SelectList(_db.Faculties, "FacultyId", "Name", department.FacultyId);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Department department = _db.Departments.Find(id);
            _db.Departments.Remove(department);
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
