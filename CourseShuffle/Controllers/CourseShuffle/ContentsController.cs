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
                var file = Request.Files["file"];
                contents.DateCreated = DateTime.Now;
                contents.DateLastModified = DateTime.Now;
                contents.CreatedBy = 1;
                contents.LastModifiedBy = 1;
                contents.CoursesId = Convert.ToInt64(collectedValues["CourseId"]);
                contents.ContentType = typeof(ContentType).GetEnumName(int.Parse(collectedValues["ContentType"]));
                var type = typeof(ContentType).GetEnumName(int.Parse(collectedValues["ContentType"]));
                if ((file != null) && (file.FileName != ""))
                    contents.File = new FileUploader().UploadFile(file, type);
                else
                    contents.LinkText = collectedValues["LinkText"];
                _db.Contents.Add(contents);
                _db.SaveChanges();
                return RedirectToAction("Create", "Contents", new {id = contents.CoursesId});
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
                contents.DateCreated =Convert.ToDateTime(collectedValues["DateCreated"]);
                contents.DateLastModified = DateTime.Now;
                contents.LastModifiedBy = 1;
                _db.Entry(contents).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
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