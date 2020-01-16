using Common;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService
    {
        private LibraryEntities libraryEntities;

        public UserService()
        {
            this.libraryEntities = new LibraryEntities();
        }

        // Get All Users
        public IQueryable<UserViewModel> GetUsers()
        {
            var userModel = libraryEntities.Users.Include("Borrows")
                .Select(x => new UserViewModel {
                    UserId = x.UserId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDate = x.BirthDate,
                    Email = x.Email,
                    Phone = x.Phone,
                    AddDate = x.AddDate,
                    ModifiedDate = x.ModifiedDate,
                    IsActive = x.IsActive,
                    BorrowedBooksCount = x.Borrows.Count(y => y.IsReturned==false),
                });
            return userModel;
        }

        // Insert User
        public void InsertUser(UserViewModel user)
        {
            User newUser = new User
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Phone = user.Phone,
                AddDate = DateTime.Now,
                ModifiedDate = user.ModifiedDate,
                IsActive = true,
            };
            libraryEntities.Users.Add(newUser);
            libraryEntities.SaveChanges();
        }

        // Get User by Id
        public UserViewModel GetUserById(int? UserId)
        {
            var user = libraryEntities.Users.Find(UserId);
            if (user == null) return null;

            UserViewModel searchedUser = new UserViewModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Phone = user.Phone,
                AddDate = user.AddDate,
                ModifiedDate = user.ModifiedDate,
                IsActive = user.IsActive,
                BorrowedBooksCount = user.Borrows.Count(y => y.IsReturned == false),
            };
            return searchedUser;
        }

        // Update User
        public void UpdateUser(UserViewModel user)
        {
            var updatedUser = libraryEntities.Users.Find(user.UserId);

            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            updatedUser.BirthDate = user.BirthDate;
            updatedUser.Email = user.Email;
            updatedUser.Phone = user.Phone;
            updatedUser.ModifiedDate = DateTime.Now;

            libraryEntities.SaveChanges();
        }

        // Soft delete User
        public void DeleteUser(int id)
        {
            User deletedUser = libraryEntities.Users.Find(id);
            deletedUser.IsActive = false;
            libraryEntities.SaveChanges();
        }

        // Get User Borrows History
        public IEnumerable<UserBooksHistoryViewModel> GetUserBorrowsHistory(int? id)
        {
            var borrows = libraryEntities.Borrows.Where(b => b.UserId == id).Join(libraryEntities.Books,
                borrow => borrow.BorrowId,
                book => book.BookId,
                (borrow, book) => new { Borrow = borrow, Book = book })
                .Select(x => new UserBooksHistoryViewModel
                {
                    BookId = x.Book.BookId,
                    Title = x.Book.Title,
                    Author = x.Book.Author,
                    ISBN = x.Book.ISBN,
                    FromDate = x.Borrow.FromDate,
                    ToDate = x.Borrow.ToDate,
                    IsReturned = x.Borrow.IsReturned
                }).ToList();

            return borrows;
        }

        // Get User Books which is non returned
        public IEnumerable<UserBooksHistoryViewModel> GetUserOwnedBooks(int? id)
        {
            return GetUserBorrowsHistory(id).Where(x => x.IsReturned == false);
        }

        // Get User Details Model
        public UserDetailsViewModel GetUsersDetailsModel(int? id)
        {
            UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel
            {
                UserModel = GetUserById(id),
                BorrowModel = GetUserBorrowsHistory(id),
                BookModel = GetUserOwnedBooks(id)
            };
            return userDetailsViewModel;
        }
    }
}
