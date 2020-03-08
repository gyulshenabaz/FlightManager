using FlightManager.Common.AutoMapping.Interfaces;
using FlightManager.Services.Models;

namespace FlightManager.Web.Areas.Flights.Models.Passengers
{
    public class PassengerListingViewModel : IMappableFrom<PassengerServiceModel>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        
        public string LastName { get; set; }
        
        public string UniqueCitizenshipNumber { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Nationality { get; set; }

        public string TicketType { get; set; }
    }
}