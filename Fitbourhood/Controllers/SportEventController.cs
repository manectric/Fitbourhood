﻿using System;
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
            SetEnumDDisciplineSelectList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SportEventListFiltered(SportEventListViewModel viewModel)
        {
            SortedList<string, string> paramDictionary = new SortedList<string, string>
            {
                { "DDisciplineID", viewModel.DDiscipline == 0 ? null : ((int)viewModel.DDiscipline).ToString() },
                { "Date", viewModel.EventDate },
                { "Time", viewModel.EventTime },
                { "HasEnded", "0"}
            };
            viewModel.SportEventList = SportEventRepository.GetSportEventsFiltered(paramDictionary);
            SetEnumDDisciplineSelectList();
            return View("Index", viewModel);
        }

        public ActionResult SportEventDetails()
        {
            return View("SportEventDetails");
        }

        private void SetEnumDDisciplineSelectList()
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            SelectListItem selListItem = new SelectListItem() { Value = "0", Text = "Wybierz dyscyplinę..." };

            var enumDDiscipline = from DDiscipline e in Enum.GetValues(typeof(DDiscipline))
                select new SelectListItem()
                {
                    Value = e.ToString(),
                    Text = EnumHelpers.GetDescriptionOfEnum(e)
                };

            newList.Add(selListItem);
            newList.AddRange(enumDDiscipline);

            ViewBag.EnumDDisciplineList = new SelectList(newList, "Value", "Text", "0");
        }
    }
}