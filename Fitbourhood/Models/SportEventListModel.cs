using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fitbourhood.Models
{
    public class SportEventListModel
    {
        public int ID { get; set; }
        public int DDisciplineID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int MaxCapacity { get; set; }
        public string Address { get; set; }
        public string CoordinateLatitude { get; set; }
        public string CoordinateLongitude { get; set; }
        public bool HasEnded { get; set; }
    }
}