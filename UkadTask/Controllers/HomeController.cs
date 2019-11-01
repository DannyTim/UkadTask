using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UkadTask.Models;

namespace UkadTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult SpeedTest(string sourceUrl)
        {
            DBService dbs = new DBService();

            try
            {
                dbs.CreateTableForURLs();
            }
            catch (SqlCeException e)
            {

            }

            if (string.IsNullOrWhiteSpace(sourceUrl))
            {
                return View(new URLInfoViewModel { URLInfo = new List<URLInfo>(), SourceUrl = sourceUrl });
            }

            SiteMapReader smr = new SiteMapReader(sourceUrl);
            smr.GetUrlsFromSiteMap();
            List<URLInfo> result = smr.MeasureResponseTime();
            dbs.AddURLInfos(result);
            return View(new URLInfoViewModel { URLInfo = result, SourceUrl = sourceUrl });
        }

        public ActionResult History(int page = 1)
        {
            DBService dbs = new DBService();
            var list = dbs.GetAllURLInfos();
            int pageSize = 100;
            IEnumerable<URLInfo> URLsPerPage = list.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = list.Count };
            return View(new HistoryViewModel() { PageInfo = pageInfo, URLInfos = URLsPerPage });
        }
    }
}