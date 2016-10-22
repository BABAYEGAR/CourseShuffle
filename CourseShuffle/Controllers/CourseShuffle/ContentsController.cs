using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Objects.Entities;
using CourseShuffle.Data.Service.Enum;
using CourseShuffle.Data.Service.FileUploader;

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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var contents = _db.Contents.Find(id);
            if (contents == null)
                return HttpNotFound();
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
        public ActionResult Create([Bind(Include = "ContentsId,Name,Author,Year")] Contents contents,
            FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
                if (loggedinuser != null)
                {
                    var file = Request.Files["file"];
                    contents.DateCreated = DateTime.Now;
                    contents.DateLastModified = DateTime.Now;
                    contents.CreatedBy = loggedinuser.AppUserId;
                    contents.LastModifiedBy = loggedinuser.AppUserId;
                    contents.CoursesId = Convert.ToInt64(collectedValues["CourseId"]);
                    contents.ContentType = typeof(ContentType).GetEnumName(int.Parse(collectedValues["ContentType"]));
                    var type = typeof(ContentType).GetEnumName(int.Parse(collectedValues["ContentType"]));
                    if ((file != null) && (file.FileName != ""))
                    {
                        contents.File = new FileUploader().UploadFile(file, type);
                    }
                    else
                    {
                        contents.LinkText = collectedValues["LinkText"];
                    }
                    _db.Contents.Add(contents);
                    _db.SaveChanges();
                    TempData["content"] = "A new content has been added to " +
                                       _db.Courseses.Find(contents.CoursesId).CourseName + "!";
                    TempData["notificationtype"] = NotificationType.Success.ToString();
                    return RedirectToAction("Create", "Contents", new {id = contents.CoursesId});
                }
                TempData["content"] = "Your session has expired,Login Again!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
            }
            return View(contents);
        }

        // GET: Contents/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var contents = _db.Contents.Find(id);
            if (contents == null)
                return HttpNotFound();
            return View(contents);
        }

        // POST: Contents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "ContentsId,Name,ContentType,Author,Year,CourseId,CreatedBy,DateCreated,File,LinkText")] Contents contents,FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
                if (loggedinuser != null)
                {
                    contents.DateCreated = Convert.ToDateTime(collectedValues["DateCreated"]);
                    contents.DateLastModified = DateTime.Now;
                    contents.LastModifiedBy = loggedinuser.AppUserId;
                    _db.Entry(contents).State = EntityState.Modified;
                    _db.SaveChanges();
                    TempData["content"] = "The Course content has been modified successfully";
                    return RedirectToAction("Index");
                }
                TempData["content"] = "Your session has expired,Login Again!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
            }
            return View(contents);
        }

        // GET: Contents/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var contents = _db.Contents.Find(id);
            if (contents == null)
                return HttpNotFound();
            return View(contents);
        }

        // POST: Contents/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var contents = _db.Contents.Find(id);
            _db.Contents.Remove(contents);
            _db.SaveChanges();
            TempData["content"] = "A course content has been deleted successfully!";
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