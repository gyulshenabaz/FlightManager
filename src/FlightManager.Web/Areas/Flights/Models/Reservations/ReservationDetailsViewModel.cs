using System.Collections.Generic;
using AutoMapper;
using FlightManager.Common.AutoMapping.Interfaces;
using FlightManager.Services.Models;
using FlightManager.Web.Areas.Flights.Models.Passengers;

namespace FlightManager.Web.Areas.Flights.Models.Reservations
{
    public class ReservationDetailsViewModel : ICustomMapping
    {
        public string Email { get; set; }
        
        public string From { get; set; }
        
        public string To { get; set; }
        
        public int FlightId { get; set; }
        
        public ICollection<PassengerListingViewModel> Passengers { get; set; }
        
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ReservationServiceModel, ReservationDetailsViewModel>()
                .ForMember(dest => dest.From, opt =>
                    opt.MapFrom(src => src.Flight.From))
                .ForMember(dest => dest.To, opt =>
                    opt.MapFrom(src => src.Flight.To));
        }
    }
}