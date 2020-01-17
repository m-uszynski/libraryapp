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

        public BorrowController()
        {
            borrowService = new BorrowService();
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

        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(BorrowCreateViewModel model)
        {
            if (model.ChoosenBooks.Contains(-1)) // empty book
            {
                ModelState.AddModelError("emptyBook", "Each list must have a book selected");
            }
            if (!ModelState.IsValid)
            {
                //return PartialView("_AddBorrowForm", model);
                var errorList = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item.Value.Errors[0].ErrorMessage).ToList();
                return Json(new { success = false, errors = errorList }, JsonRequestBehavior.AllowGet);
            }

            borrowService.InsertBorrows(model);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}