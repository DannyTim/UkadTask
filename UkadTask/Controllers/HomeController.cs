﻿using System;
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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void Test()
        {
            DBtest db = new DBtest();
            db.Connect();
            db.AddTable();
            //var a = ConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString;
        }
    }
}