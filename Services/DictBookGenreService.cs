using Common;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DictBookGenreService
    {
        private LibraryEntities libraryEntities;

        public DictBookGenreService()
        {
            libraryEntities = new LibraryEntities();
        }

        public IEnumerable<DictBookGenreViewModel> getDictBookGenres()
        {
            var model = (from genres in libraryEntities.DictBookGenres
                         select new DictBookGenreViewModel {
                             BookGenreId = genres.BookGenreId,
                             Name = genres.Name.TrimEnd()
                         });

            return model;
        }
    }
}
