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
            IQueryable<User> users = libraryEntities.Users.Include("Borrows");
            var userModel = users.Select(x => new UserViewModel {
                UserId = x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate,
                Email = x.Email,
                Phone = x.Phone,
                AddDate = x.AddDate,
                ModifiedDate = x.ModifiedDate,
                IsActive = x.IsActive,
                Borrows = x.Borrows
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
                Borrows = user.Borrows
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
    }
}
