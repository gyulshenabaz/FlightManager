using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FlightManager.Common.AutoMapping.Interfaces;
using FlightManager.Services.Models;

namespace FlightManager.Web.Areas.Flights.Models.Reservations
{
    public class ReservationListingViewModel :  IMappableFrom<ReservationServiceModel>
    {
        public int Id { get; set; }

        public string Email { get; set; }
        
        public int FlightId { get; set; }

        public int PassengersCount { get; set; }
        
    }
}