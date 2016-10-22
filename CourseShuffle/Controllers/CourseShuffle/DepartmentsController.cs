using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Factory.FactoryData;
using CourseShuffle.Data.Objects.Entities;
using CourseShuffle.Data.Service.Enum;

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
        public ActionResult ViewDepartmentForFaculty(long id)
        {
            var departments = new DepartmentFactory().GetAllDepartmentsForAFaculty(id);
            return View("FacultyDepartments", departments);
        }

        // GET: Departments/Create
        public ActionResult Create(long? id)
        {
            if (id <= 0)
            {
                id = 0;
            }
            ViewBag.FacultyId = new SelectList(_db.Faculties, "FacultyId", "Name");
            ViewBag.Faculty = id ?? 0;

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
                var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
                if (loggedinuser != null)
                {
                    var facultyId = Convert.ToInt64(collectedValues["FacultyId"]);
                    if (collectedValues["FacultyId"] == null)
                    {
                        facultyId = Convert.ToInt64(collectedValues["Faculty"]);
                    }

                    department.FacultyId = facultyId;

                    department.DateCreated = DateTime.Now;
                    department.DateLastModified = DateTime.Now;
                    department.CreatedBy = loggedinuser.AppUserId;
                    department.LastModifiedBy = loggedinuser.AppUserId;
                    _db.Departments.Add(department);
                    _db.SaveChanges();
                    TempData["user"] = "A new department has been created successfully!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Index");
                }
                TempData["department"] = "Your session has expired,Login Again!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
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

                var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
                if (loggedinuser != null)
                {
                    department.DateCreated = Convert.ToDateTime(collectedValues["DateCreated"]);
                department.DateLastModified = DateTime.Now;
                    department.LastModifiedBy = loggedinuser.AppUserId;
                _db.Entry(department).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
                }
                TempData["department"] = "Your session has expired,Login Again!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
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
            TempData["department"] = "A department has been deleted successfully!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
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
