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
using CourseShuffle.Data.Service.Enum;
using CourseShuffle.Data.Service.FileUploader;

namespace CourseShuffle.Controllers.CourseShuffle
{
    public class ProjectsController : Controller
    {
        private ProjectDataContext _db = new ProjectDataContext();

        // GET: Projects
        public ActionResult Index()
        {
            var projects = _db.Projects.Include(p => p.Session);
            return View(projects.ToList());
        }
        // GET: Projects
        public ActionResult GetProjectsForDepartment(long departmentId)
        {
            var projects = _db.Projects.Include(p => p.Session).Where(n=>n.DepartmentId == departmentId );
            return View("Index",projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.SessionId = new SelectList(_db.Sessions, "SessionId", "Name");
            ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,Title,Author,StartDate,EndDate,CategoryId,SessionId,DepartmentId")] Project project,FormCollection collectedValues)
        {
            var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if (loggedinuser != null)
                {
                    HttpPostedFileBase document = Request.Files["document"];
                    project.Document = new FileUploader().UploadFile(document,
    UploadType.ProjectDocument.ToString());
                    project.DateCreated = DateTime.Now;
                    project.DateLastModified = DateTime.Now;
                    project.CreatedBy = loggedinuser.AppUserId;
                    project.LastModifiedBy = loggedinuser.AppUserId;
                    _db.Projects.Add(project);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                TempData["project"] = "Your session has expired,Login Again!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("Index");
            }

            ViewBag.SessionId = new SelectList(_db.Sessions, "SessionId", "Name", project.SessionId);
            ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name", project.DepartmentId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.SessionId = new SelectList(_db.Sessions, "SessionId", "Name", project.SessionId);
            ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name",project.DepartmentId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,Title,Author,CategoryId,DepartmentId,Document,SessionId,CreatedBy")] Project project,FormCollection collectedValues)
        {
            var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if (loggedinuser != null)
                {
                    project.StartDate = Convert.ToDateTime(collectedValues["StartDate"]);
                    project.EndDate = Convert.ToDateTime(collectedValues["EndDate"]);
                    project.DateCreated = Convert.ToDateTime(collectedValues["DateCreated"]);
                    project.DateLastModified = DateTime.Now;
                    project.LastModifiedBy = loggedinuser.AppUserId;
                    _db.Entry(project).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                TempData["project"] = "Your session has expired,Login Again!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("Index");
            }
    
                ViewBag.SessionId = new SelectList(_db.Sessions, "SessionId", "Name", project.SessionId);
            ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name", project.DepartmentId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Project project = _db.Projects.Find(id);
            _db.Projects.Remove(project);
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
