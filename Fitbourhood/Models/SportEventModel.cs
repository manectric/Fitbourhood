using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [RegularExpression("([0-9]*)", ErrorMessage = "Count must be a natural number")]
        public int? MaxCapacity { get; set; }
        public string CoordinateLatitude { get; set; }
        public string CoordinateLongitude { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool HasEnded { get; set; } = false;
        public bool IsCreateMode { get; set; } = true;
    }
}