using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fitbourhood.Dictionaries;

namespace Fitbourhood.Models
{
    public class SportEventModel
    {
        public int ID { get; set; }
        public int CreatorID { get; set; }
        public DDiscipline DDiscipline { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int MaxCapacity { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public bool HasEnded { get; set; }
        public bool IsCreateMode { get; set; }
    }
}