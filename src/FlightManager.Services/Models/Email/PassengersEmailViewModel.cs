using AutoMapper;
using FlightManager.Common.AutoMapping.Interfaces;
using FlightManager.Models;
using FlightManager.Services.Models;

namespace FlightManager.Services.Models.Email
{
    public class PassengersEmailViewModel : ICustomMapping
    {
        public string FullName { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Passenger, PassengersEmailViewModel>()
                .ForMember(dest => dest.FullName, opt =>
                    opt.MapFrom(src => $"{src.FirstName} {src.MiddleName} {src.LastName}"));
        }
    }
}