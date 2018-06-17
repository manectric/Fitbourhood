using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fitbourhood.Models
{
    public class Notification
    {
        public int UserID { get; set; }
        public int SportEventID { get; set; }
        public string Description { get; set; }
        public bool IsSended { get; set; }
        public bool IsAppproved { get; set; }
}
}