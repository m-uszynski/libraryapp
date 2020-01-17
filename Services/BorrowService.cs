using Common;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BorrowService
    {
        private LibraryEntities libraryEntities;

        public BorrowService()
        {
            this.libraryEntities = new LibraryEntities();
        }

        public IEnumerable<BorrowViewModel> GetUserBorrowsHistory(int? UserId)
        {
            var borrows = libraryEntities.Borrows.Where(b => b.UserId == UserId);
            var borrowModel = borrows.Select(x => new BorrowViewModel
            {
                BorrowId = x.BorrowId,
                UserId = x.UserId,
                BookId = x.BookId,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                IsReturned = x.IsReturned,
                WhoBorrow = x.User.FirstName+" "+x.User.LastName
            });
            return borrowModel;
        }

        public IEnumerable<BorrowViewModel> GetBorrowsInBook(int bookId)
        {
            var borrows = libraryEntities.Borrows.Where(b => b.BookId == bookId);
            var borrowModel = borrows.Select(x => new BorrowViewModel {
                BorrowId = x.BorrowId,
                UserId = x.UserId,
                BookId = x.BookId,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                IsReturned = x.IsReturned,
                WhoBorrow = x.User.FirstName + " " + x.User.LastName
            });
            return borrowModel;
        }

        public IEnumerable<BorrowViewModel> GetCurrentBorrowsInBook(int bookId)
        {
            var borrows = libraryEntities.Borrows.Where(b => b.BookId == bookId && b.IsReturned == false);
            var borrowModel = borrows.Select(x => new BorrowViewModel {
                BorrowId = x.BorrowId,
                UserId = x.UserId,
                BookId = x.BookId,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                IsReturned = x.IsReturned,
                WhoBorrow = x.User.FirstName + " " + x.User.LastName
            });
            return borrowModel;
        }

        public IEnumerable<BorrowGridViewModel> GetCurrentBookBorrows()
        {
            var model = (from borrow in libraryEntities.Borrows
                         join book in libraryEntities.Books on borrow.BookId equals book.BookId
                         join user in libraryEntities.Users on borrow.UserId equals user.UserId
                         where borrow.IsReturned == false
                         select new BorrowGridViewModel
                         {
                             BookId = book.BookId,
                             Author = book.Author,
                             Title = book.Title,
                             ISBN = book.ISBN,
                             BorrowId = borrow.BorrowId,
                             FromDate = borrow.FromDate,
                             ToDate = borrow.ToDate,
                             UserId = user.UserId,
                             WhoBorrowed = user.FirstName+" "+user.LastName,
                             Email = user.Email,
                             Phone = user.Phone
                         }).ToList();
            return model;
        }

        public IEnumerable<UserViewModel> GetUserWhoHaveBooks()
        {

            var model = (from borrow in libraryEntities.Borrows
                         where borrow.IsReturned == false
                         group borrow by borrow.UserId into x
                         join user in libraryEntities.Users on x.FirstOrDefault().UserId equals user.UserId
                         select new UserViewModel
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
                             BorrowedBooksCount = user.Borrows.Count(b=>b.IsReturned==false)
                         }).ToList();

            return model;
        }

        public void InsertBorrows(BorrowCreateViewModel model)
        {
            // Insert Borrow
            libraryEntities.Configuration.AutoDetectChangesEnabled = false;
            foreach(var book in model.ChoosenBooks)
            {
                libraryEntities.Borrows.Add(new Borrow
                {
                    UserId = model.UserId,
                    BookId = book,
                    FromDate = DateTime.Now,
                    ToDate = model.ToDate,
                    IsReturned = false
                });
            }
            libraryEntities.SaveChanges();
            libraryEntities.Configuration.AutoDetectChangesEnabled = true;

            // Decrement count
            var books = libraryEntities.Books.Where(x => model.ChoosenBooks.Contains(x.BookId)).ToList();
            books.ForEach(x => x.Count = x.Count - 1);
            libraryEntities.SaveChanges();
        }
    }
}
