using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

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

        public JsonResult GetJsonBooks()
        {
            var books = service.GetBooks();
            List<BookViewModel> bookViewmodel = new List<BookViewModel>();
            foreach(var book in books)
            {
                bookViewmodel.Add(new BookViewModel
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Author = book.Author,
                    ReleaseDate = book.ReleaseDate,
                    ISBN = book.ISBN,
                    BookGenreId = book.BookGenreId,
                    Count = book.Count,
                    AddDate = book.AddDate,
                    ModifiedDate = book.ModifiedDate
                });
            }
            return Json(bookViewmodel, JsonRequestBehavior.AllowGet);
        }

    }
}