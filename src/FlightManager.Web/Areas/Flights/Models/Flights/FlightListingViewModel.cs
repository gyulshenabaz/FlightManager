using System;
using System.Collections.Generic;
using FlightManager.Common.AutoMapping.Interfaces;
using FlightManager.Services.Models;
using FlightManager.Web.Areas.Flights.Models.Reservations;

namespace FlightManager.Web.Areas.Flights.Models.Flights
{
    public class FlightListingViewModel : IMappableFrom<FlightServiceModel>
    {
        public string Id { get; set; }

        public string From { get; set; }
        
        public string To { get; set; }
        
        public DateTime DepartureDate { get; set; }
        
        public DateTime ArrivalDate { get; set; }
        
        public string AircraftType { get; set; }
        
        public string AircraftRegistration { get; set; }
        
        public int AvailableSeats { get; set; }
        
        public ICollection<ReservationListingViewModel> Reservations { get; set; }
    }
}