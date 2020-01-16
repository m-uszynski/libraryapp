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

        public IQueryable<DictBookGenreViewModel> getDictBookGenres()
        {
            IQueryable<DictBookGenre> genres = libraryEntities.DictBookGenres;
            var dictBookGenreModel = genres.Select(x => new DictBookGenreViewModel {
                BookGenreId = x.BookGenreId,
                Name = x.Name
            });
            return dictBookGenreModel;
        }
    }
}
