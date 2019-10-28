using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UkadTask.Models;

namespace UkadTask.Controllers
{
    public class DiagnosticsController : Controller
    {
        [HttpGet]
        public ActionResult Get(string sourceUrl)
        {
            DBService dbs = new DBService();
            if (string.IsNullOrWhiteSpace(sourceUrl))
            {
                //DBService dbs = new DBService();
                return View("~/Views/View.cshtml", new URLInfoViewModel { URLInfo = dbs.GetAllURLInfos("abcdef"), SourceUrl = sourceUrl });
            }

            //SiteMapReader smr = new SiteMapReader(sourceUrl);
            //List<URLInfo> result = smr.MeasureResponseTime();

            List<URLInfo> exampleList = new List<URLInfo>();

            //exampleList.Add(new URLInfo() { Url = "aaa", ElapsedTime = 111 });

            //dbs.CreateTableForURLs(sourceUrl);
            //dbs.AddURLInfos(exampleList, sourceUrl);
            //dbs.GetURLInfos(sourceUrl);
            //dbs.GetByNameURLInfos(sourceUrl, "c");

            //return View("~/Views/Shared/Error.cshtml");
            return View("~/Views/View.cshtml", new URLInfoViewModel{URLInfo = dbs.GetAllURLInfos(sourceUrl), SourceUrl = sourceUrl});
        }
    }
}
//{ y: 100, indexLabel: "lowest", markerColor: "DarkSlateGrey", markerType: "cross" },
//{ y: 520, indexLabel: "highest",markerColor: "red", markerType: "triangle" },