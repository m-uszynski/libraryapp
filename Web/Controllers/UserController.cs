using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;
using Common;
using System.Data;
using System.Net;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private UserService userService;
        private BookService bookService;
        private BorrowService borrowService;

        public UserController()
        {
            userService = new UserService();
            bookService = new BookService();
            borrowService = new BorrowService();
        }

        // GET: User
        public ActionResult Index()
        {
            var users = userService.GetUsers();
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName, LastName, BirthDate, Email, Phone")] UserViewModel userViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userService.InsertUser(userViewModel);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again... ");
            }
            return View(userViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = userService.GetUserById(id);
            if (user == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId, FirstName, LastName, BirthDate, Email, Phone")] UserViewModel userViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userService.UpdateUser(userViewModel);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again... ");
            }
            return View(userViewModel);
        }

        [HttpGet]
        public ActionResult SoftDelete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            UserViewModel user = userService.GetUserById(id);
            if (user == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(user);
        }

        [HttpPost, ActionName("SoftDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult SoftDeleteConfirmed(int id)
        {
            userService.DeleteUser(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var userDetailsViewModel = userService.GetUsersDetailsModel(id);
            return View(userDetailsViewModel);
        }

        public JsonResult GetActiveUser()
        {
            var activeUsers = userService.GetActiveUser();
            return Json(activeUsers, JsonRequestBehavior.AllowGet);
        }
    }
}