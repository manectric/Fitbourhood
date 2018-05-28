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
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int MaxCapacity { get; set; }
        public string Location { get; set; }
        public bool HasEnded { get; set; }
    }
}