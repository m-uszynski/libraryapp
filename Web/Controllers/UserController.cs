using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;
using Common;
using System.Data;
using DAL;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private Service service;

        public UserController()
        {
            this.service = new Service();
        }

        #region Index
        // GET: User
        public ActionResult Index()
        {
            var users = service.GetUsers().Where(u=>u.IsActive==true);
            return View(users);
        }
        #endregion

        #region Create
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
                    User newUser = new User
                    {
                        FirstName = userViewModel.FirstName,
                        LastName = userViewModel.LastName,
                        BirthDate = userViewModel.BirthDate,
                        Email = userViewModel.Email,
                        Phone = userViewModel.Phone,
                        AddDate = DateTime.Now,
                        IsActive = true
                    };

                    service.InsertUser(newUser);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again... ");
            }
            return View(userViewModel);
        }
        #endregion

        #region Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = service.GetUserById(id);
            UserViewModel userViewModel = new UserViewModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Phone = user.Phone
            };
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId, FirstName, LastName, BirthDate, Email, Phone")] UserViewModel userViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = service.GetUserById(userViewModel.UserId);
                    user.FirstName = userViewModel.FirstName;
                    user.LastName = userViewModel.LastName;
                    user.BirthDate = userViewModel.BirthDate;
                    user.Email = userViewModel.Email;
                    user.Phone = userViewModel.Phone;
                    user.ModifiedDate = DateTime.Now;

                    service.UpdateUser(user);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again... ");
            }
            return View(userViewModel);
        }
        #endregion

        #region SoftDelete
        [HttpGet]
        public ActionResult SoftDelete(int id)
        {
            User user = service.GetUserById(id);
            return View(user);
        }

        [HttpPost, ActionName("SoftDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult SoftDeleteConfirmed(int id)
        {
            User user = service.GetUserById(id);
            user.IsActive = false;
            service.UpdateUser(user);
            return RedirectToAction("Index");
        }
        #endregion

        #region Details
        public ViewResult Details(int id)
        {
            User user = service.GetUserById(id);
            UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel
            {
                User = user,
                Borrow = service.GetBorrowsHistory(id),
                Book = service.GetOwnedBooks(id)
            };
            return View(userDetailsViewModel);
        }
        #endregion
    }
}