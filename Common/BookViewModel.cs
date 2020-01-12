using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public string ISBN { get; set; }
        public int BookGenreId { get; set; }
        public int Count { get; set; }
        public System.DateTime AddDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
