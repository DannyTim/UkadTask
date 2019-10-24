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
            if (string.IsNullOrWhiteSpace(sourceUrl))
            {
                return View("~/Views/View.cshtml");
            }

            //SiteMapReader smr = new SiteMapReader(sourceUrl);
            //List<URLInfo> result = smr.MeasureResponseTime();

            List<URLInfo> exampleList = new List<URLInfo>();

            //exampleList.Add(new URLInfo() { Url = "aaa", ElapsedTime = 111 });
            //exampleList.Add(new URLInfo() { Url = "bbb", ElapsedTime = 222 });
            //exampleList.Add(new URLInfo() { Url = "ccc", ElapsedTime = 333 });
            //exampleList.Add(new URLInfo() { Url = "ddd", ElapsedTime = 444 });
            //exampleList.Add(new URLInfo() { Url = "eee", ElapsedTime = 555 });
            //exampleList.Add(new URLInfo() { Url = "ece", ElapsedTime = 666 });
            //exampleList.Add(new URLInfo() { Url = "cec", ElapsedTime = 777 });
            //exampleList.Add(new URLInfo() { Url = "eec", ElapsedTime = 888 });
            //exampleList.Add(new URLInfo() { Url = "eecaa", ElapsedTime = 999 });
            //exampleList.Add(new URLInfo() { Url = "eecaaecaa", ElapsedTime = 100 });
            //exampleList.Add(new URLInfo() { Url = "bbbbbbbbbbbb", ElapsedTime = 101 });
            //exampleList.Add(new URLInfo() { Url = "bbbbbbbbbbbb", ElapsedTime = 101 });
            //exampleList.Add(new URLInfo() { Url = "bbbbbbbbbbbb", ElapsedTime = 101 });


            DBService dbs = new DBService();

            //dbs.CreateTableForURLs(sourceUrl);
            //dbs.AddURLInfos(exampleList, sourceUrl);
            //dbs.GetURLInfos(sourceUrl);
            //dbs.GetByNameURLInfos(sourceUrl, "c");
            

            return View("~/Views/View.cshtml", new URLInfoViewModel{URLInfo = dbs.GetByNameURLInfos(sourceUrl, "a"), SourceUrl = sourceUrl});
        }
    }
}