using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using FlightManager.Common.AutoMapping.Interfaces;
using FlightManager.Services.Models;
using FlightManager.Web.Areas.Flights.Models.Reservations;

namespace FlightManager.Web.Areas.Flights.Models.Flights
{
    public class FlightDetailsViewModel : IMappableFrom<FlightServiceModel>, IMappableTo<FlightServiceModel>
    {
        public int Id { get; set; }
        
        public string From { get; set; }
        
        public string To { get; set; }
        
        public DateTime DepartureDate { get; set; }
        
        public DateTime ArrivalDate { get; set; }
        
        public string AircraftType { get; set; }

        public string AircraftRegistration { get; set; }

        public string PilotName { get; set; }
        
        public int AvailableSeats { get; set; }

        public int BusinessClassSeats { get; set; }

        public int EconomyClassSeats { get; set; }
        
        public ICollection<ReservationListingViewModel> Reservations { get; set; }
    }
}