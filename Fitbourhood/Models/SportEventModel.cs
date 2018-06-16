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
        [Required(ErrorMessage = "Wybór dyscypliny jest wymagany")]
        public DDiscipline DDiscipline { get; set; }
        [Required(ErrorMessage = "Data wydarzenia jest wymagana")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Godzina wydarzenia jest wymagana")]
        public string Time { get; set; }
        [Required(ErrorMessage = "Maksymalna liczba uczesnitków jest wymagana")]
        [RegularExpression("([0-9]*)")]
        public int? MaxCapacity { get; set; }
        public string CoordinateLatitude { get; set; }
        public string CoordinateLongitude { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool HasEnded { get; set; } = false;
        public bool IsCreateMode { get; set; } = true;
        public List<ContactDetails> ContactList { get; set; }
    }
}