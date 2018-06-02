using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fitbourhood.Models
{
    public class UserContextModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }
}