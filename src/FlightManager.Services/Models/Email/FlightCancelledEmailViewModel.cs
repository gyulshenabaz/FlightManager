using AutoMapper;
using FlightManager.Common.AutoMapping.Interfaces;
using FlightManager.Models;

namespace FlightManager.Services.Models.Email
{
    public class FlightCancelledEmailViewModel : ICustomMapping
    {
        public string From { get; set; }
        
        public string To { get; set; }
        
        public string DepartureDate { get; set; }
        
        public string ArrivalDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Flight, FlightCancelledEmailViewModel>()
                .ForMember(dest => dest.DepartureDate, opt =>
                    opt.MapFrom(src => src.DepartureDate.ToString("dd-MM-yyyy HH:mm")))
                .ForMember(dest => dest.ArrivalDate, opt =>
                    opt.MapFrom(src => src.ArrivalDate.ToString("dd-MM-yyyy HH:mm")));
        }
    }
}