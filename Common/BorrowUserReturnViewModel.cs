﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class BorrowUserReturnViewModel
    {
        public int BookId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }

        public int BorrowId { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }

        //public int UserId { get; set; }
        //public string WhoBorrowed { get; set; }
    }
}
