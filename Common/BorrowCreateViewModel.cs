using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class BorrowCreateViewModel
    {
        [Range(1, Int32.MaxValue, ErrorMessage = "Please choose User")]
        public int UserId { get; set;}
        public int[] ChoosenBooks { get; set; }
        [Range(typeof(DateTime), "1/1/2020", "1/1/2050", ErrorMessage = "Date of birth must be between 01.01.2020 and 01.01.2050")]
        public System.DateTime ToDate { get; set; }
    }
}
