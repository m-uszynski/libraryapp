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

        public IEnumerable<UserViewModel> GetUsers()
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

        public void InsertUser(UserViewModel user)
        {
            var newUser = new User
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

        public UserViewModel GetUserById(int? UserId)
        {
            var user = libraryEntities.Users.Find(UserId);
            if (user == null) return null;

            var searchedUser = new UserViewModel
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

        public void DeleteUser(int? id)
        {
            var deletedUser = libraryEntities.Users.Find(id);
            deletedUser.IsActive = false;
            libraryEntities.SaveChanges();
        }

        public IEnumerable<UserBooksHistoryViewModel> GetUserBorrowsHistory(int? id)
        {
            var borrows = (from borrow in libraryEntities.Borrows
                           join book in libraryEntities.Books on borrow.BookId equals book.BookId
                           where borrow.UserId == id
                           select new UserBooksHistoryViewModel
                           {
                               BookId = book.BookId,
                               Title = book.Title,
                               Author = book.Author,
                               ISBN = book.ISBN,
                               FromDate = borrow.FromDate,
                               ToDate = borrow.ToDate,
                               IsReturned = borrow.IsReturned
                           });

            return borrows;
        }

        public IEnumerable<UserBooksHistoryViewModel> GetUserOwnedBooks(int? id)
        {
            return GetUserBorrowsHistory(id).Where(x => x.IsReturned == false);
        }

        public UserDetailsViewModel GetUsersDetailsModel(int? id)
        {
            var userDetailsViewModel = new UserDetailsViewModel
            {
                UserModel = GetUserById(id),
                BorrowModel = GetUserBorrowsHistory(id),
                BookModel = GetUserOwnedBooks(id)
            };
            if (userDetailsViewModel.UserModel == null || userDetailsViewModel.BookModel == null || userDetailsViewModel.BorrowModel == null) return null;
            return userDetailsViewModel;
        }

        public IEnumerable<UserToBorrowViewModel> GetActiveUser()
        {
            var model =  libraryEntities.Users.Where(x => x.IsActive == true).Select(x => new UserToBorrowViewModel
            {
               UserId = x.UserId,
               UserName = x.FirstName + " " + x.LastName
            });
            return model;
        }
    }
}
