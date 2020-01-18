using Common;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ReportService
    {
        private LibraryEntities libraryEntities;

        public ReportService()
        {
            this.libraryEntities = new LibraryEntities();
        }

        public IEnumerable<ReportUserViewModel> GetPageableMostActiveUsers(int skip, int take, int pageSize, out int totalCount)
        {
            var model = (from borrow in libraryEntities.Borrows
                         group borrow by borrow.UserId into x
                         join user in libraryEntities.Users on x.FirstOrDefault().UserId equals user.UserId
                         select new ReportUserViewModel
                         {
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             BorrowCount = x.Count()
                         }).OrderByDescending(x => x.BorrowCount);
            totalCount = model.Count();
            return model.Skip(skip).Take(take);
        }

        public IEnumerable<ReportUserViewModel> GetPageableMostActiveUserWithFilter(int skip, int take, int pageSize, string filterLastNameValue, out int totalCount)
        {
            var model = (from borrow in libraryEntities.Borrows
                         group borrow by borrow.UserId into x
                         join user in libraryEntities.Users on x.FirstOrDefault().UserId equals user.UserId
                         where user.LastName.Substring(0, filterLastNameValue.Length) == filterLastNameValue
                         select new ReportUserViewModel
                         {
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             BorrowCount = x.Count()
                         }).OrderByDescending(x => x.BorrowCount);
            totalCount = model.Count();
            return model.Skip(skip).Take(take);
        }

        //public IEnumerable<ReportBooksViewModel> GetPageableMostOftenBorrowedBooks(int skip, int take, int pageSize, out int totalCount)
        //{
        //    var model = (from borrow in libraryEntities.Borrows
        //                 group borrow by borrow.BookId into x
        //                 join book in libraryEntities.Books on x.FirstOrDefault().BookId equals book.BookId
        //                 select new ReportBooksViewModel
        //                 {
        //                     Title = book.Title,
        //                     Author = book.Author,
        //                     Genre = book.DictBookGenre.Name,
        //                     GenreId = book.BookGenreId,
        //                     BorrowCount = x.Count()
        //                 }).OrderByDescending(x => x.BorrowCount);
        //    totalCount = model.Count();
        //    return model.Skip(skip).Take(take);
        //}

        public IEnumerable<ReportBooksViewModel> GetPageableMostOftenBorrowedBooksWithFilter(int skip, int take, int pageSize, string title, DateTime? fromdate, DateTime? todate, int? genreid, out int totalCount)
        {
            var allBorrow = libraryEntities.Borrows.Select(x => new BorrowViewModel {
                BorrowId = x.BorrowId,
                BookId = x.BookId,
                UserId = x.UserId,
                FromDate = x.FromDate,
                ToDate = x.ToDate
            });

            if (fromdate != null) allBorrow = allBorrow.Where(x => x.FromDate >= fromdate);
            if (todate != null) allBorrow = allBorrow.Where(x => x.FromDate < todate);

            var model = (from borrow in allBorrow
                         group borrow by borrow.BookId into x
                         join book in libraryEntities.Books on x.FirstOrDefault().BookId equals book.BookId
                         select new ReportBooksViewModel
                         {
                             Title = book.Title,
                             Author = book.Author,
                             Genre = book.DictBookGenre.Name,
                             GenreId = book.BookGenreId,
                             BorrowCount = x.Count()
                         });

            if (title != "" && title != null) model = model.Where(x => x.Title.Contains(title));
            if (genreid != null) model = model.Where(x => x.GenreId == genreid);

            var finalModel = model.OrderByDescending(x => x.BorrowCount);
            totalCount = finalModel.Count();
            return finalModel.Skip(skip).Take(take);
        }

    }
}
