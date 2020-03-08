using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using FlightManager.Common.AutoMapping.Interfaces;
using FlightManager.Models;
using FlightManager.Services.Models;

namespace FlightManager.Services.Models.Email
{
    public class ReservationEmailViewModel : ICustomMapping
    {
        public string Email { get; set; }
        public string From { get; set; }
        
        public string To { get; set; }
        
        public string DepartureDate { get; set; }
        
        public string ArrivalDate { get; set; }
        
        public ICollection<PassengersEmailViewModel> Passengers { get; set; }
        
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Reservation, ReservationEmailViewModel>()
                .ForMember(dest => dest.From, opt =>
                    opt.MapFrom(src => src.Flight.From))
                .ForMember(dest => dest.To, opt =>
                    opt.MapFrom(src => src.Flight.To))
                .ForMember(dest => dest.DepartureDate, opt =>
                    opt.MapFrom(src => src.Flight.DepartureDate.ToString("dd-MM-yyyy HH:mm")))
                .ForMember(dest => dest.ArrivalDate, opt =>
                    opt.MapFrom(src => src.Flight.ArrivalDate.ToString("dd-MM-yyyy HH:mm")));
        }
    }
}