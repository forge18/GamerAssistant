﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamerAssistant.Web.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            return View();
        }
    }
}