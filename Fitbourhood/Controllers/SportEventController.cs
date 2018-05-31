using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fitbourhood.Dictionaries;
using Fitbourhood.Helpers;
using Fitbourhood.Models;
using Fitbourhood.Repositories;

namespace Fitbourhood.Controllers
{
    public class SportEventController : Controller
    {
        // GET: SportEvent
        public ActionResult Index()
        {
            SportEventListViewModel viewModel = new SportEventListViewModel();
            viewModel.SportEventList = SportEventRepository.GetAllSportEvents();
            SetEnumDDisciplineSelectList(true);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SportEventListFiltered(SportEventListViewModel viewModel)
        {
            SortedList<string, string> paramDictionary = new SortedList<string, string>();

            if (viewModel.DDiscipline != 0)
                paramDictionary.Add("DDisciplineID", EnumHelpers.GetDescriptionOfEnum(ComparisionEnum.Equal) + " " + (int)viewModel.DDiscipline);
            if(!string.IsNullOrWhiteSpace(viewModel.EventDate))
                paramDictionary.Add("Date", EnumHelpers.GetDescriptionOfEnum(ComparisionEnum.Equal) + " '" + viewModel.EventDate + "'");
            if (!string.IsNullOrWhiteSpace(viewModel.EventTime))
                paramDictionary.Add("Time", EnumHelpers.GetDescriptionOfEnum(ComparisionEnum.GreaterEqual) + " '" + viewModel.EventTime + "'");
            paramDictionary.Add("HasEnded", EnumHelpers.GetDescriptionOfEnum(ComparisionEnum.Equal) + " 0");

            viewModel.SportEventList = SportEventRepository.GetSportEventsFiltered(paramDictionary);
            SetEnumDDisciplineSelectList(true);
            return View("Index", viewModel);
        }

        [HttpGet]
        public ActionResult SportEvent()
        {
            SportEventModel model = new SportEventModel();
            SetEnumDDisciplineSelectList(false);
            return View("SportEvent", model);
        }

        [HttpPost]
        public ActionResult SportEvent(SportEventModel model)
        {
            return View("SportEvent");
        }

        private void SetEnumDDisciplineSelectList(bool isSearchList)
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            if (isSearchList)
            {
                SelectListItem selListItem = new SelectListItem() { Value = "0", Text = "Wybierz dyscyplinę..." };
                newList.Add(selListItem);
            }

            var enumDDiscipline = from DDiscipline e in Enum.GetValues(typeof(DDiscipline))
                                  select new SelectListItem()
                                  {
                                      Value = e.ToString(),
                                      Text = EnumHelpers.GetDescriptionOfEnum(e)
                                  };

            newList.AddRange(enumDDiscipline);

            ViewBag.EnumDDisciplineList = new SelectList(newList, "Value", "Text", "0");
        }
    }
}