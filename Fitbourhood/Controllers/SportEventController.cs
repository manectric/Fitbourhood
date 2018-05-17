using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fitbourhood.Models;
using Fitbourhood.Repositories;

namespace Fitbourhood.Controllers
{
    public class SportEventController : Controller
    {
        // GET: SportEvent
        public ActionResult Index()
        {
            List<SportEvent> sportEventList = new List<SportEvent>();
            sportEventList = SportEventRepository.GetAllSportEvents();
            return View(sportEventList);
        }

        public ActionResult SportEventDetails()
        {
            return View("SportEventDetails");
        }
    }
}