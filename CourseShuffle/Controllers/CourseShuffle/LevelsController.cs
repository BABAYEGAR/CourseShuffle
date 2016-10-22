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
    public class LevelsController : Controller
    {
        private readonly LevelDataContext _db = new LevelDataContext();

        // GET: Levels
        public ActionResult Index()
        {
            return View(_db.Levels.ToList());
        }

        // GET: Levels/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Level level = _db.Levels.Find(id);
            if (level == null)
            {
                return HttpNotFound();
            }
            return View(level);
        }

        // GET: Levels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Levels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LevelId,Name")] Level level)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
                level.DateCreated = DateTime.Now;
                level.DateLastModified = DateTime.Now;
                if (loggedinuser != null) level.LastModifiedBy = loggedinuser.AppUserId;
                level.CreatedBy = 1;
                _db.Levels.Add(level);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(level);
        }

        // GET: Levels/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Level level = _db.Levels.Find(id);
            if (level == null)
            {
                return HttpNotFound();
            }
            return View(level);
        }

        // POST: Levels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LevelId,Name,DateCreated,DateLastModified")] Level level)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
                if (loggedinuser != null) level.LastModifiedBy = loggedinuser.AppUserId;
                level.DateLastModified = DateTime.Now;
                _db.Entry(level).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(level);
        }

        // GET: Levels/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Level level = _db.Levels.Find(id);
            if (level == null)
            {
                return HttpNotFound();
            }
            return View(level);
        }

        // POST: Levels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Level level = _db.Levels.Find(id);
            _db.Levels.Remove(level);
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
