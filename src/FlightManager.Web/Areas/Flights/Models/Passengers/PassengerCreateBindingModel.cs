using System.ComponentModel.DataAnnotations;
using FlightManager.Common.AutoMapping.Interfaces;
using FlightManager.Services.Models;

namespace FlightManager.Web.Areas.Flights.Models.Passengers
{
    public class PassengerCreateBindingModel : IMappableTo<PassengerServiceModel>
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string MiddleName { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        
        [Required]
        [RegularExpression(@"^\d{10}$")]
        public string UniqueCitizenshipNumber { get; set; }
        
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Nationality { get; set; }
        
        [Required]
        public string TicketType { get; set; }
    }
}