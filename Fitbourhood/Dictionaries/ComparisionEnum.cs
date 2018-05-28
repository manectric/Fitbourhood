using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fitbourhood.Helpers
{
    public enum ComparisionEnum
    {
        [Display(Description = "=")]
        Equal,
        [Display(Description = "<>")]
        NotEqual,
        [Display(Description = ">")]
        Greater,
        [Display(Description = ">=")]
        GreaterEqual,
        [Display(Description = "<")]
        Less,
        [Display(Description = "<=")]
        LessEqual,

    }
}