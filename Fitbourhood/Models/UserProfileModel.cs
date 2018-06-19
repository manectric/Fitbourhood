using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fitbourhood.Models
{
    public class UserProfileModel
    {
        public int DeclaredSportEvents { get; set; }
        public int ApprovedSportEvents { get; set; }
        public double Percentage { get; set; }
        public string OldPassword { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Hasło musi zawierać od 8 do 15 znaków<br/>Minimum 1 znak specjalny, cyfrę, małe oraz duże litery")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Podane hasła muszą się zgadzać")]
        public string NewPasswordRepeat { get; set; }
    }
}