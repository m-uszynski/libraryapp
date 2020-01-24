using Common;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public JsonResult GetCurrentBorrows()
        {
            var currentBorrows = borrowService.GetCurrentBookBorrows();
            return Json(currentBorrows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsersWhoHaveBook()
        {
            var currentBorrows = borrowService.GetUserWhoHaveBooks();
            return Json(currentBorrows, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReturnBorrow(int id)
        {
            borrowService.ReturnBook(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReturnBorrows(BorrowListViewModel model)
        {
            borrowService.ReturnBooks(model.BorrowsId);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCurrentUserBorrow(int id)
        {
            var model = borrowService.GetCurrentUserBorrows(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult Create()
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

        public ActionResult UserReturn(int id)
        {
            var model = userService.GetUserById(id);
            return View(model);
        }
    }
}