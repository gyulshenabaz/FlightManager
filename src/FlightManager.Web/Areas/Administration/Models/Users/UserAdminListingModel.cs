using AutoMapper;
using FlightManager.Common.AutoMapping.Interfaces;
using FlightManager.Models;

namespace FlightManager.Web.Areas.Administration.Models.Users
{
    public class UserAdminListingModel : ICustomMapping
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UniqueCitizenshipNumber { get; set; }
        
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<FlightManagerUser, UserAdminListingModel>()
                .ForMember(dest => dest.FullName, opt =>
                    opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

        }
    }
}