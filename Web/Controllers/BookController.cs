using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using DAL;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class BookController : Controller
    {
        private Service service;

        public BookController()
        {
            this.service = new Service();
        }

        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        #region JSON
        public string GetBooks()
        {
            var books = service.GetBooks();
            var json = JsonConvert.SerializeObject(books, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            return json;
        }

        public string GetBorrowsInBook(int id)
        {
            var borrows = service.GetBorrowsInBook(id);
            var json = JsonConvert.SerializeObject(borrows, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            return json;
        }

        public string GetCurrentBorrowInBook(int id)
        {
            var borrows = service.GetBorrowsInBook(id).Where(b => b.IsReturned == false);
            var json = JsonConvert.SerializeObject(borrows, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            return json;
        }

        public JsonResult GetBookViewModelById(int id)
        {
            var book = service.GetBookById(id);
            BookViewModel bookViewModel = new BookViewModel
            {
                Title = book.Title,
                Author = book.Author,
                ReleaseDate = book.ReleaseDate,
                ISBN = book.ISBN,
                BookGenreId = book.BookGenreId,
                Count = book.Count,
                AddDate = book.AddDate,
                ModifiedDate = book.ModifiedDate
            };
            return Json(bookViewModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpGet]
        public PartialViewResult Create()
        {
            ViewBag.genres = service.GetDictBookGenre();
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.genres = service.GetDictBookGenre();
                return PartialView("_AddEditBookForm", bookViewModel);
            }

            Book newBook = new Book
            {
                Title = bookViewModel.Title,
                Author = bookViewModel.Author,
                ReleaseDate = bookViewModel.ReleaseDate,
                ISBN = bookViewModel.ISBN,
                BookGenreId = bookViewModel.BookGenreId,
                Count = bookViewModel.Count,
                AddDate = DateTime.Now
            };
            service.InsertBook(newBook);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            var book = service.GetBookById(id);
            ViewBag.genres = service.GetDictBookGenre();

            BookViewModel bookViewModel = new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                ReleaseDate = book.ReleaseDate,
                ISBN = book.ISBN,
                BookGenreId = book.BookGenreId,
                Count = book.Count,
                AddDate = book.AddDate,
                ModifiedDate = DateTime.Now
            };
            return PartialView(bookViewModel);
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.genres = service.GetDictBookGenre();
                return PartialView("_AddEditBookForm", bookViewModel);
            }

            Book book = service.GetBookById(bookViewModel.BookId);
            book.Title = bookViewModel.Title;
            book.Author = bookViewModel.Author;
            book.ReleaseDate = bookViewModel.ReleaseDate;
            book.ISBN = bookViewModel.ISBN;
            book.BookGenreId = bookViewModel.BookGenreId;
            book.Count = bookViewModel.Count;
            book.ModifiedDate = DateTime.Now;

            service.UpdateBook(book);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ViewResult Details(int id)
        {
            Book book = service.GetBookById(id);
            BookViewModel bookViewModel = new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                ReleaseDate = book.ReleaseDate,
                ISBN = book.ISBN,
                GenreName = service.GetDictBookGenreName(book.BookGenreId),
                Count = book.Count,
                AddDate = book.AddDate,
                ModifiedDate = book.ModifiedDate,
                BorrowCount = 3
            };
            return View(bookViewModel);
        }
    }
}