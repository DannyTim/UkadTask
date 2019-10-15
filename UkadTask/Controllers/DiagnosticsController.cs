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
        public ActionResult Get(string url)
        {
            SiteMapReader smr = new SiteMapReader(url);
            var result = smr.MeasureResponseTime();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}