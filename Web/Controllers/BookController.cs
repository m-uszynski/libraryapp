using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class BookController : Controller
    {
        private BookService bookService;
        private BorrowService borrowService;
        private DictBookGenreService dictBookGenreService;

        public BookController()
        {
            bookService = new BookService();
            borrowService = new BorrowService();
            dictBookGenreService = new DictBookGenreService();
        }

        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetBooks()
        {
            var books = bookService.GetBooks();
            return Json(books, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBorrowsInBook(int id)
        {
            var borrows = borrowService.GetBorrowsInBook(id);
            return Json(borrows, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCurrentBorrowInBook(int id)
        {
            var borrows = borrowService.GetCurrentBorrowsInBook(id);
            return Json(borrows, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBookViewModelById(int id)
        {
            var book = bookService.GetBookById(id);
            return Json(book, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.genres = dictBookGenreService.getDictBookGenres();
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.genres = dictBookGenreService.getDictBookGenres();
                return PartialView("_AddBookForm", bookViewModel);
            }

            bookService.InsertBook(bookViewModel);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var bookViewModel = bookService.GetBookById(id);
            ViewBag.genres = dictBookGenreService.getDictBookGenres();
            return PartialView(bookViewModel);
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.genres = dictBookGenreService.getDictBookGenres();
                return PartialView("_EditBookForm", bookViewModel);
            }

            bookService.UpdateBook(bookViewModel);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            var bookViewModel = bookService.GetBookById(id);
            return View(bookViewModel);
        }

        public ActionResult GetBookUserCanBorrow(int id)
        {
            var bookUserCanBorrow = bookService.GetBookUserCanBorrow(id);
            return Json(bookUserCanBorrow, JsonRequestBehavior.AllowGet);
        }
    }
}