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
    public class ContentsController : Controller
    {
        private readonly ContentDataContext _db = new ContentDataContext();

        // GET: Contents
        public ActionResult Index()
        {
            var contents = _db.Contents.Include(c => c.Courses);
            return View(contents.ToList());
        }

        // GET: Contents/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contents contents = _db.Contents.Find(id);
            if (contents == null)
            {
                return HttpNotFound();
            }
            return View(contents);
        }

        // GET: Contents/Create
        public ActionResult Create(long id)
        {
            ViewBag.CourseId = id;
            return View();
        }

        // POST: Contents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContentsId,Name,ContentType,Author,Year,CourseId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] Contents contents)
        {
            if (ModelState.IsValid)
            {
                _db.Contents.Add(contents);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contents);
        }

        // GET: Contents/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contents contents = _db.Contents.Find(id);
            if (contents == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(_db.Courseses, "CoursesId", "CourseName", contents.CourseId);
            ViewBag.ContentType = new SelectList(_db.Courseses, "CoursesId", "CourseName", contents.CourseId);
            return View(contents);
        }

        // POST: Contents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContentsId,Name,ContentType,Author,Year,CourseId,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] Contents contents)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(contents).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(_db.Courseses, "CoursesId", "CourseName", contents.CourseId);
            return View(contents);
        }

        // GET: Contents/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contents contents = _db.Contents.Find(id);
            if (contents == null)
            {
                return HttpNotFound();
            }
            return View(contents);
        }

        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Contents contents = _db.Contents.Find(id);
            _db.Contents.Remove(contents);
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
