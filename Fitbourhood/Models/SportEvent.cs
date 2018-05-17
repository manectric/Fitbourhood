using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fitbourhood.Models
{
    public class SportEvent
    {
        public int ID { get; set; }
        public int CreatorID { get; set; }
        public int DDisciplineID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int MaxCapacity { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public bool HasEnded { get; set; }
    }
}