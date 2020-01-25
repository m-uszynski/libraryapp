using Common;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class BorrowController : Controller
    {
        private BorrowService borrowService;
        private UserService userService;

        public BorrowController()
        {
            borrowService = new BorrowService();
            userService = new UserService();
        }

        // GET: Borrow
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCurrentBorrows()
        {
            var currentBorrows = borrowService.GetCurrentBookBorrows();
            return Json(currentBorrows, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUsersWhoHaveBook()
        {
            var currentBorrows = borrowService.GetUserWhoHaveBooks();
            return Json(currentBorrows, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ReturnBorrow(int id)
        {
            borrowService.ReturnBook(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ReturnBorrows(BorrowListViewModel model)
        {
            borrowService.ReturnBooks(model.BorrowsId);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCurrentUserBorrow(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var model = borrowService.GetCurrentUserBorrows(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(BorrowCreateViewModel model)
        {
            if (model.ChoosenBooks.Contains(-1))
            {
                ModelState.AddModelError("emptyBook", "Each list must have a book selected");
            }
            if (model.ChoosenBooks.Length != model.ChoosenBooks.Distinct().Count())
            {
                ModelState.AddModelError("haveDuplicate", "It isn't possible to borrow two identical books.");
            }
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item.Value.Errors[0].ErrorMessage).ToList();
                return Json(new { success = false, errors = errorList }, JsonRequestBehavior.AllowGet);
            }

            borrowService.InsertBorrows(model);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserReturn(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var model = userService.GetUserById(id);
            if (model == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(model);
        }
    }
}