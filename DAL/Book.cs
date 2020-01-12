//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.Borrows = new HashSet<Borrow>();
        }
    
        public int BookId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public string ISBN { get; set; }
        public int BookGenreId { get; set; }
        public int Count { get; set; }
        public System.DateTime AddDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual DictBookGenre DictBookGenre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Borrow> Borrows { get; set; }
    }
}
