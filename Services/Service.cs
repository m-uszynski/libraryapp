using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace Services
{
    public class Service
    {
        private LibraryEntities libraryEntities;

        public Service()
        {
            this.libraryEntities = new LibraryEntities();
            libraryEntities.Configuration.LazyLoadingEnabled = false;
            libraryEntities.Configuration.ProxyCreationEnabled = false;
        }

        // User service
        public IEnumerable<User> GetUsers()
        {
            return libraryEntities.Users.ToList();
        }

        public void InsertUser(User user)
        {
            libraryEntities.Users.Add(user);
            libraryEntities.SaveChanges();
        }

        public User GetUserById(int UserId)
        {
            return libraryEntities.Users.Find(UserId);
        }

        public void UpdateUser(User user)
        {
            libraryEntities.Entry(user).State = EntityState.Modified;
            libraryEntities.SaveChanges();
        }

        // Book service
        public void UpdateBook(Book book)
        {
            libraryEntities.Entry(book).State = EntityState.Modified;
            libraryEntities.SaveChanges();
        }

        public IEnumerable<Book> GetBooks()
        {
            return libraryEntities.Books.Include(b=>b.DictBookGenre).ToList();
        }

        public IEnumerable<Book> GetOwnedBooks(int userId)
        {
            List<Book> books = new List<Book>();
            var borrows = libraryEntities.Borrows.Where(b => b.UserId == userId && b.IsReturned == false).ToList();
            foreach(var borrow in borrows)
            {
                books.Add(libraryEntities.Books.Where(b => b.BookId == borrow.BookId).FirstOrDefault());
            }
            return books;
        }

        public void InsertBook(Book book)
        {
            libraryEntities.Books.Add(book);
            libraryEntities.SaveChanges();
        }

        public Book GetBookById(int BookId)
        {
            return libraryEntities.Books.Find(BookId);
        }

        public int GetCountOfBorrowedBooks(int id)
        {
            var book = libraryEntities.Books.Find(id);
            int count = book.Borrows.Where(b => b.IsReturned == false).Count();
            return count;
        }

        // Borrow service
        public IEnumerable<Borrow> GetBorrowsHistory(int UserId)
        {
            return libraryEntities.Borrows.Where(b => b.UserId == UserId).ToList();
        }

        public IEnumerable<Borrow> GetBorrowsInBook(int BookId)
        {
            return libraryEntities.Borrows.Where(b => b.BookId == BookId).Include(b => b.User).ToList();
        }

        // Book Genre service
        public IEnumerable<DictBookGenre> GetDictBookGenre()
        {
            return libraryEntities.DictBookGenres.ToList();
        }

        public string GetDictBookGenreName(int BookGenreId)
        {
            return libraryEntities.DictBookGenres.Where(d => d.BookGenreId == BookGenreId).FirstOrDefault().Name;
        }
    }
}
