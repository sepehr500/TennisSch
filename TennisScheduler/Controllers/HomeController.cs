﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisScheduler.Classes;

namespace TennisScheduler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var calander = new TennisCalender();
            
            var z = calander.getDay(3, 11 , 2015);
            var test = z.GroupBy(x => x.Time.TimeOfDay).OrderBy(x => x.Key).ToList();
            var Cal = new CalenderShow();
            var model = Cal.getFullCalender(DateTime.Now.AddMonths(1));
            //var model = Cal.getCalender(DateTime.Now.Month, DateTime.Now.Year);
            return View(model);

            
        }
        public ActionResult AsyncUpdateCalender(int month, int year)
        {
            if (HttpContext.Request.IsAjaxRequest())
            {
                var Cal = new CalenderShow();
                var model = Cal.getCalender(month, year);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }
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
    }
}