using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class BookViewModel
    {
        public int BookId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Author")]
        [MaxLength(50, ErrorMessage = "Autor cannot be longer than 50 characters")]
        public string Author { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Title")]
        [MaxLength(50, ErrorMessage = "Title cannot be longer than 50 characters")]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1/1/1700", "1/1/2020", ErrorMessage = "Release Data must be between 01.01.1700 and 01.01.2020")]
        public Nullable<System.DateTime> ReleaseDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "ISBN")]
        [MaxLength(50, ErrorMessage = "ISBN cannot be longer than 50 characters")]
        public string ISBN { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int BookGenreId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue,ErrorMessage = "The Count cannot be less than zero")]
        public int Count { get; set; }

        [Required]
        [Display(Name = "Add Time")]
        [DataType(DataType.Date)]
        public System.DateTime AddDate { get; set; }

        [Display(Name = "Modified Time")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Display(Name = "Genre")]
        public string GenreName { get; set; }
    }
}
