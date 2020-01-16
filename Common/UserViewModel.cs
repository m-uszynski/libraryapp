using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "First name")]
        [MaxLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Last name")]
        [MaxLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2020", ErrorMessage = "Date of birth must be between 01.01.1900 and 01.01.2020" )]
        public System.DateTime BirthDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50, ErrorMessage = "E-mail cannot be longer than 50 characters")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(20, ErrorMessage = "Phone cannot be longer than 50 characters")]
        public string Phone { get; set; }

        [Display(Name = "Add Date")]
        [DataType(DataType.Date)]
        public System.DateTime AddDate { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public virtual ICollection<Borrow> Borrows { get; set; }

    }
}
