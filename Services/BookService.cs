using Common;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{


    public class BookService
    {
        private LibraryEntities libraryEntities;

        public BookService()
        {
            this.libraryEntities = new LibraryEntities();
        }

        public IQueryable<BookViewModel> GetBooks()
        {
            IQueryable<Book> books = libraryEntities.Books;
            var bookModel = books.Select(x => new BookViewModel {
                BookId = x.BookId,
                Author = x.Author,
                Title = x.Title,
                ReleaseDate = x.ReleaseDate,
                ISBN = x.ISBN,
                BookGenreId = x.BookGenreId,
                Count = x.Count,
                AddDate = x.AddDate,
                ModifiedDate = x.ModifiedDate,
                GenreName = libraryEntities.DictBookGenres.Where(b => b.BookGenreId == x.BookGenreId).FirstOrDefault().Name
            });
            return bookModel;
        }

        public BookViewModel GetBookById(int bookId)
        {
            var book = libraryEntities.Books.Find(bookId);
            BookViewModel searchedBook = new BookViewModel
            {
                BookId = book.BookId,
                Author = book.Author,
                Title = book.Title,
                ReleaseDate = book.ReleaseDate,
                ISBN = book.ISBN,
                BookGenreId = book.BookGenreId,
                Count = book.Count,
                AddDate = book.AddDate,
                ModifiedDate = book.ModifiedDate,
                GenreName = libraryEntities.DictBookGenres.Where(b => b.BookGenreId == book.BookGenreId).FirstOrDefault().Name
            };
            return searchedBook;
        }

        public IEnumerable<BookViewModel> GetOwnedBooksByUserId(int? userId)
        {
            var books = libraryEntities.Books.Where(x => x.Borrows.Any(y => y.UserId == userId && y.IsReturned == false));   
            var bookModel = books.Select(x => new BookViewModel {
                BookId = x.BookId,
                Author = x.Author,
                Title = x.Title,
                ReleaseDate = x.ReleaseDate,
                ISBN = x.ISBN,
                BookGenreId = x.BookGenreId,
                Count = x.Count,
                AddDate = x.AddDate,
                ModifiedDate = x.ModifiedDate,
                GenreName = libraryEntities.DictBookGenres.Where(b=>b.BookGenreId==x.BookGenreId).FirstOrDefault().Name
            });
            return bookModel;
        }

        public void InsertBook(BookViewModel book)
        {
            Book newBook = new Book
            {
                Title = book.Title,
                Author = book.Author,
                ReleaseDate = book.ReleaseDate,
                ISBN = book.ISBN,
                BookGenreId = book.BookGenreId,
                Count = book.Count,
                AddDate = DateTime.Now
            };
            libraryEntities.Books.Add(newBook);
            libraryEntities.SaveChanges();
        }

        public void UpdateBook(BookViewModel book)
        {
            Book updatedBook = libraryEntities.Books.Find(book.BookId);
            updatedBook.Title = book.Title;
            updatedBook.Author = book.Author;
            updatedBook.ReleaseDate = book.ReleaseDate;
            updatedBook.ISBN = book.ISBN;
            updatedBook.BookGenreId = book.BookGenreId;
            updatedBook.Count = book.Count;
            updatedBook.ModifiedDate = DateTime.Now;
            libraryEntities.SaveChanges();
        }

        public IEnumerable<BookToBorrowViewModel> GetBookUserCanBorrow(int UserId)
        {
            var ownedBooks = (from borrow in libraryEntities.Borrows
                              join book in libraryEntities.Books on borrow.BookId equals book.BookId
                              where borrow.UserId == UserId && borrow.IsReturned == false
                              select new BookToBorrowViewModel
                              {
                                  BookId = book.BookId,
                                  Title = book.Title
                              });

            var availableBooks = (from book in libraryEntities.Books
                                  where book.Count > 0
                                  select new BookToBorrowViewModel
                                  {
                                      BookId = book.BookId,
                                      Title = book.Title
                                  });
                              
            return availableBooks.Except(ownedBooks);
        }
    }
}
