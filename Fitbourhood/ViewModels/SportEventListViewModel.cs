using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fitbourhood.Dictionaries;

namespace Fitbourhood.Models
{
    public class SportEventListViewModel
    {
        public List<SportEvent> SportEventList { get; set; }
        public DDiscipline DDiscipline { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
    }
}