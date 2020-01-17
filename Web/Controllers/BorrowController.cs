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
    }
}