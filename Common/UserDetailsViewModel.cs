using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class UserDetailsViewModel
    {
        public UserViewModel UserModel { get; set; }
        public IEnumerable<BookViewModel> BookModel { get; set; }
        public IEnumerable<BorrowViewModel> BorrowModel { get; set; }
    }
}
