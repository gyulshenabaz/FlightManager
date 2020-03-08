using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using FlightManager.Common.AutoMapping.Interfaces;
using FlightManager.Models;
using FlightManager.Services.Models;
using FlightManager.Web.Areas.Flights.Models.Flights;
using FlightManager.Web.Areas.Flights.Models.Passengers;

namespace FlightManager.Web.Areas.Flights.Models.Reservations
{
    public class ReservationCreateBindingModel : IMappableTo<ReservationServiceModel>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public int FlightId { get; set; }
        
        public FlightDetailsViewModel Flight { get; set; }
        
        public ICollection<PassengerCreateBindingModel> Passengers { get; set; }
        
    }
}