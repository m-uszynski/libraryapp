using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ReportController : Controller
    {
        private ReportService reportService;

        public ReportController()
        {
            reportService = new ReportService();
        }

        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetMostActiveUsers(int skip, int take, int pageSize, string filter)
        {
            int totalUserCount;

            if (!String.IsNullOrEmpty(filter) && filter != "null")
            {
                var filterLastNameValue = JsonConvert.DeserializeObject<FilterContainer>(filter).filters.FirstOrDefault().value;
                if(filterLastNameValue != null)
                {
                    var sortableByLastNamePageUser = reportService.GetPageableMostActiveUserWithFilter(skip, take, pageSize, filterLastNameValue, out totalUserCount);
                    return Json(new { total = totalUserCount, data = sortableByLastNamePageUser }, JsonRequestBehavior.AllowGet);
                }
            }

            var pageUser = reportService.GetPageableMostActiveUsers(skip, take, pageSize, out totalUserCount);
            return Json(new { total = totalUserCount, data = pageUser }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMostOftenBooks(int skip, int take, int pageSize, string title, DateTime? fromdate, DateTime? todate, int? genreid)
        {
            int totalBookCount;

            var pageBooks = reportService.GetPageableMostOftenBorrowedBooksWithFilter(skip, take, pageSize, title, fromdate, todate, genreid, out totalBookCount);
            return Json(new { total = totalBookCount, data = pageBooks }, JsonRequestBehavior.AllowGet);
        }

        class FilterContainer
        {
            public List<FilterDescription> filters { get; set; }
            public string logic { get; set; }
        }

        class FilterDescription
        {
            public string @operator { get; set; }
            public string field { get; set; }
            public string value { get; set; }
        }

    }
}