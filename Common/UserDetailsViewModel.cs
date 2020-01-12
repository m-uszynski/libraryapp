﻿using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class UserDetailsViewModel
    {
        public User User { get; set; }
        public IEnumerable<Book> Book { get; set; }
        public IEnumerable<Borrow> Borrow { get; set; }
    }
}
