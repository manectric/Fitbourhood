using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fitbourhood.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Imię jest wymagane")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]{9})$", ErrorMessage = "Niepoprawny format numeru telefonu")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Adres E-mail jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Ponowne podanie hasła jest wymagane")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Podane hasła muszą się zgadzać")]
        public string PasswordRepeat { get; set; }
    }
}