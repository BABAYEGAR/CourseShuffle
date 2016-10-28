using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Factory.FactoryData;
using CourseShuffle.Data.Objects.Entities;
using CourseShuffle.Data.Service.Enum;

namespace CourseShuffle.Controllers.CourseShuffle
{
    public class CategoriesController : Controller
    {
        private readonly CategoryDataContext db = new CategoryDataContext();

        // GET: Categories
        public ActionResult Index()
        {
            var categories = db.Categories.Include(c => c.Department);
            return View(categories.ToList());
        }
        /// <summary>
        ///     Sends Json responds object to view with categories of the department requested via an Ajax call
        /// </summary>
        /// <param name="id"> state id</param>
        /// <returns>lgas</returns>
        public JsonResult GetCategoryForDepartment(int id)
        {
            var categories = new CategoryFactory().GetDepartmentCategories(id);
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
        // GET: Categories/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var category = db.Categories.Find(id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name,DepartmentId")] Category category)
        {
            var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if ((loggedinuser != null) && (loggedinuser.Role == UserType.Administrator.ToString()))
                {
                    category.DateCreated = DateTime.Now;
                    category.DateLastModified = DateTime.Now;
                    category.CreatedBy = loggedinuser.AppUserId;
                    category.LastModifiedBy = loggedinuser.AppUserId;
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                TempData["category"] = "A new category has been created!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", category.DepartmentId);
            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var category = db.Categories.Find(id);
            if (category == null)
                return HttpNotFound();
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", category.DepartmentId);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "CategoryId,Name,DepartmentId,CreatedBy,DateCreated")] Category category)
        {
            var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
            if (ModelState.IsValid)
            {
                if ((loggedinuser != null) && (loggedinuser.Role == UserType.Administrator.ToString()))
                {
                    category.DateLastModified = DateTime.Now;
                    category.LastModifiedBy = loggedinuser.AppUserId;
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["category"] = "The category has been modified!";
                    TempData["notificationtype"] = NotificationType.Success.ToString();
                    return RedirectToAction("Index");
                }
                TempData["category"] = "Your session has expired,Login Again!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", category.DepartmentId);
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var category = db.Categories.Find(id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}