using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UkadTask.Models;

namespace UkadTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult History(int page = 1)
        {
            DBService dbs = new DBService();
            var list = dbs.GetAllURLInfos("abcdef");
            int pageSize = 100; // количество объектов на страницу
            IEnumerable<URLInfo> URLsPerPage = list.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = list.Count };
            HistoryViewModel ivm = new HistoryViewModel() { PageInfo = pageInfo, URLInfos = URLsPerPage };
            return View(ivm);
        }
    }
}

//TODO: move Diagnostics to Home