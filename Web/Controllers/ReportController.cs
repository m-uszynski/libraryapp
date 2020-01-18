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

            if (filter != null && filter != "" && filter != "null")
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

        public JsonResult GetMostOftenBooks(int skip, int take, int pageSize, string title, string fromdate, string todate)
        {
            int totalBookCount;

            DateTime temp;
            DateTime? FromDate = null;
            DateTime? ToDate = null;
            FromDate = DateTime.TryParse(fromdate, out temp) ? temp : (DateTime?)null;
            ToDate = DateTime.TryParse(todate, out temp) ? temp : (DateTime?)null;

            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("FromDate: " + FromDate);
            System.Diagnostics.Debug.WriteLine("ToDate: " + ToDate);
            System.Diagnostics.Debug.WriteLine("");


            //var pageBooks = reportService.GetPageableMostOftenBorrowedBooks(skip, take, pageSize, out totalBookCount);
            var pageBooks = reportService.GetPageableMostOftenBorrowedBooksWithFilter(skip, take, pageSize, title, FromDate, ToDate, out totalBookCount);
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