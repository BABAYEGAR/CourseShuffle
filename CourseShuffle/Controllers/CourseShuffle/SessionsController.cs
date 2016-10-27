using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Objects.Entities;
using CourseShuffle.Data.Service.Enum;

namespace CourseShuffle.Controllers.CourseShuffle
{
    public class SessionsController : Controller
    {
        private readonly SessionDataContext _db = new SessionDataContext();

        // GET: Sessions
        public ActionResult Index()
        {
            return View(_db.Sessions.ToList());
        }

        // GET: Sessions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var session = _db.Sessions.Find(id);
            if (session == null)
                return HttpNotFound();
            return View(session);
        }

        // GET: Sessions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SessionId,Name,StartDate,EndDate")] Session session)
        {
            var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
            if (!ModelState.IsValid) return View(session);
            if (loggedinuser != null)
            {
                session.CreatedBy = loggedinuser.AppUserId;
                session.LastModifiedBy = loggedinuser.AppUserId;
                session.DateCreated = DateTime.Now;
                session.DateLastModified = DateTime.Now;
                _db.Sessions.Add(session);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["session"] = "Your session has expired,Login Again!";
            TempData["notificationtype"] = NotificationType.Info.ToString();
            return View(session);
        }

        // GET: Sessions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var session = _db.Sessions.Find(id);
            if (session == null)
                return HttpNotFound();
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "SessionId,Name,StartDate,EndDate,CreatedBy,DateCreated,")] Session session)
        {
            var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if (loggedinuser != null)
                {
                    session.DateLastModified = DateTime.Now;
                    session.LastModifiedBy = loggedinuser.AppUserId;
                    _db.Entry(session).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }

                TempData["session"] = "Your session has expired,Login Again!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
            }
            return View(session);
        }

        // GET: Sessions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var session = _db.Sessions.Find(id);
            if (session == null)
                return HttpNotFound();
            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var session = _db.Sessions.Find(id);
            _db.Sessions.Remove(session);
            _db.SaveChanges();
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