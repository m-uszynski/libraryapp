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

        public string GetBooks()
        {
            var books = bookService.GetBooks();
            var json = JsonConvert.SerializeObject(books, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            return json;
        }

        public string GetBorrowsInBook(int id)
        {
            var borrows = borrowService.GetBorrowsInBook(id);
            var json = JsonConvert.SerializeObject(borrows, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            return json;
        }

        public string GetCurrentBorrowInBook(int id)
        {
            var borrows = borrowService.GetCurrentBorrowsInBook(id);
            var json = JsonConvert.SerializeObject(borrows, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            return json;
        }

        public JsonResult GetBookViewModelById(int id)
        {
            var book = bookService.GetBookById(id);
            return Json(book, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult Create()
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
        public PartialViewResult Edit(int id)
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

        public ViewResult Details(int id)
        {
            var bookViewModel = bookService.GetBookById(id);
            return View(bookViewModel);
        }

        public JsonResult GetBookUserCanBorrow(int id)
        {
            var bookUserCanBorrow = bookService.GetBookUserCanBorrow(id);
            return Json(bookUserCanBorrow, JsonRequestBehavior.AllowGet);
        }
    }
}