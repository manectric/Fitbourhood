using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fitbourhood.Controllers
{
    public class SportEventController : Controller
    {
        // GET: SportEvent
        public ActionResult Index()
        {
            return View("SportEventList");
        }

        public ActionResult SportEventDetails()
        {
            return View("SportEventDetails");
        }
    }
}