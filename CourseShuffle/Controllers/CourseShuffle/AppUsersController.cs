using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Factory.FactoryData;
using CourseShuffle.Data.Objects.Entities;
using CourseShuffle.Data.Service.EmailService;
using CourseShuffle.Data.Service.Encryption;
using CourseShuffle.Data.Service.Enum;
using CourseShuffle.Data.Service.FileUploader;
using CourseShuffle.Data.Service.TextFormatter;

namespace CourseShuffle.Controllers.CourseShuffle
{
    public class AppUsersController : Controller
    {
        private readonly AppUserDataContext _db = new AppUserDataContext();

        // GET: AppUsers
        public ActionResult Index()
        {
            return View(_db.AppUsers.ToList());
        }

        // GET: AppUsers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var appUser = _db.AppUsers.Find(id);
            if (appUser == null)
                return HttpNotFound();
            return View(appUser);
        }

        // GET: AppUsers/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(
                 Include = "AppUserId,Firstname,Lastname,Othername,Email,MobileNumber,DepartmentId"
             )] AppUser appUser, FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
                if ((loggedinuser != null) && (loggedinuser.Role == UserType.Administrator.ToString()))
                {
                    var profileImage = Request.Files["avatar-2"];
                    appUser.DateCreated = DateTime.Now;
                    appUser.DateLastModified = DateTime.Now;
                    appUser.LastModifiedBy = loggedinuser.AppUserId;
                    appUser.CreatedBy = loggedinuser.AppUserId;
                    appUser.Role = typeof(UserType).GetEnumName(int.Parse(collectedValues["Role"]));
                    var password = Membership.GeneratePassword(8, 1);
                    var hashPassword = new Md5Ecryption().ConvertStringToMd5Hash(password.Trim());
                    appUser.Password = new RemoveCharacters().RemoveSpecialCharacters(hashPassword);
                    appUser.ProfilePicture = new FileUploader().UploadFile(profileImage,
                        UploadType.ProfileImage.ToString());
                    _db.AppUsers.Add(appUser);
                    var userExist = new AppUserFactory().CheckIfGeneralUserExist(appUser.Email.Trim());
                    if (userExist)
                    {
                        TempData["user"] = "This user email already exist,try a different email!";
                        TempData["notificationtype"] = NotificationType.Danger.ToString();
                        return View(appUser);
                    }
                    _db.SaveChanges();
                    appUser.Password = password;
                    new MailerDaemon().NewUser(appUser);
                    TempData["user"] = "A new user has been created!";
                    TempData["notificationtype"] = NotificationType.Success.ToString();
                    return RedirectToAction("Index");
                }
                TempData["user"] = "Your session has expired,Login Again!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var appUser = _db.AppUsers.Find(id);
            ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
            ViewBag.LevelId = new SelectList(_db.Levels, "LevelId", "Name");
            var roles = new SelectList(typeof(UserType).GetEnumNames());
            ViewBag.roles = roles;
            if (appUser == null)
                return HttpNotFound();
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(
                 Include =
                     "AppUserId,Firstname,Lastname,Othername,Email,Password,MobileNumber,DepartmentId,Role,CreatedBy,DateCreated"
             )] AppUser appUser, FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["courseshuffleloggedinuser"] as AppUser;
                if (loggedinuser != null)
                {
                    var profileImage = Request.Files["avatar-2"];
                    if ((profileImage != null) && (profileImage.FileName == ""))
                        appUser.ProfilePicture = collectedValues["ProfilePicture"];
                    else
                        appUser.ProfilePicture = new FileUploader().UploadFile(profileImage,
                            UploadType.ProfileImage.ToString());
                    appUser.DateLastModified = DateTime.Now;
                    appUser.LastModifiedBy = loggedinuser.AppUserId;
                    _db.Entry(appUser).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    TempData["user"] = "Your session has expired,Login Again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                }
                TempData["user"] = "The user details has been modified successfully!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
            ViewBag.LevelId = new SelectList(_db.Levels, "LevelId", "Name");
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var appUser = _db.AppUsers.Find(id);
            if (appUser == null)
                return HttpNotFound();
            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var appUser = _db.AppUsers.Find(id);
            _db.AppUsers.Remove(appUser);
            _db.SaveChanges();
            TempData["user"] = "A user has been deleted successfully!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }
        // GET: AppUsers/EditProfle/5
        public ActionResult EditProfile(long id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var appUser = _db.AppUsers.Find(id);
            if (appUser == null)
                return HttpNotFound();
            var roles = new SelectList(typeof(UserType).GetEnumNames());
            ViewBag.roles = roles;
            return View(appUser);
        }
        // POST: AppUsers/EditProfile/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(
            [Bind(
                 Include =
                     "AppUserId,Firstname,Lastname,Email,MobileNumber,Password")
            ] AppUser appUser, FormCollection collectedValues)
        {
            HttpPostedFileBase profileImage = Request.Files["avatar-2"];
            if (ModelState.IsValid)
            {
                if (profileImage != null && profileImage.FileName == "")
                {
                    appUser.ProfilePicture = collectedValues["image"];
                }
                else
                {
                    appUser.ProfilePicture = new FileUploader().UploadFile(profileImage, UploadType.ProfileImage.ToString());
                }
                appUser.DateCreated = Convert.ToDateTime(collectedValues["date"]);
                appUser.DateLastModified = DateTime.Now;
                appUser.CreatedBy = long.Parse(collectedValues["createdby"]);
                appUser.Password = collectedValues["password"];
                appUser.LastModifiedBy = appUser.AppUserId;
                appUser.Role = collectedValues["role"];
                _db.Entry(appUser).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["profile"] = "You have modified your profile details successfully!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("ProfileDetails", "Account", new { Id = appUser.AppUserId });
            }
            return View(appUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}