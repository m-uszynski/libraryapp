﻿using Common;
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
    }
}