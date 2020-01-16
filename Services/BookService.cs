using Common;
using DAL;
using System;
using System.Collections.Generic;
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
        
        // Get user' owned books
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
                DictBookGenre = x.DictBookGenre
            });

            return bookModel;
        }
    }
}
