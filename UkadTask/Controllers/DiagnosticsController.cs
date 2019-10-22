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

            SiteMapReader smr = new SiteMapReader(sourceUrl);
            List<URLInfo> result = smr.MeasureResponseTime();
            return View("~/Views/View.cshtml", new URLInfoViewModel{URLInfo = result, SourceUrl = sourceUrl});
        }
    }
}