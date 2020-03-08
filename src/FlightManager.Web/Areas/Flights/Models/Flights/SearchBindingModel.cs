using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.Areas.Flights.Models.Flights
{
    public class SearchBindingModel
    {
        [Required(ErrorMessage = "Въведете място на излитане")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Това поле трябва да съдържа поне 4 букви")]
        public string From { get; set; }
        
        [Required(ErrorMessage = "Въведете дестинация")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Това поле трябва да съдържа поне 4 букви")]
        public string To { get; set; }
        
        public DateTime Date { get; set; }

        public IEnumerable<FlightListingViewModel> FoundFlights { get; set; }
    }
}