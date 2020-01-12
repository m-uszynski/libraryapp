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
        public IEnumerable<Book> GetBooks()
        {
            return libraryEntities.Books;
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

        // Borrow service
        public IEnumerable<Borrow> GetBorrowsHistory(int UserId)
        {
            return libraryEntities.Borrows.Where(b => b.UserId == UserId).ToList();
        }
    }
}
