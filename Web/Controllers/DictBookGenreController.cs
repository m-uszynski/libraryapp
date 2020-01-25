using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class DictBookGenreController : Controller
    {
        private DictBookGenreService dictBookGenreService;

        public DictBookGenreController()
        {
            dictBookGenreService = new DictBookGenreService();
        }

        public ActionResult GetBookDictGenre()
        {
            var model = dictBookGenreService.getDictBookGenres();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
         
    }
}