using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fitbourhood.Dictionaries
{
    public enum DDiscipline
    {
        [Display(Description = "Koszykówka")]
        Basketball = 1,
        [Display(Description = "Kolarstwo")]
        Cycling = 2,
        [Display(Description = "Bieganie")]
        Running = 3,
        [Display(Description = "Piłka nożna")]
        Soccer = 4,
        [Display(Description = "Siatkówka")]
        Volleyball = 5
    }
}