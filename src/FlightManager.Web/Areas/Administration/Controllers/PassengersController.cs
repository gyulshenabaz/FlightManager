using System.Linq;
using System.Threading.Tasks;
using FlightManager.Common.AutoMapping;
using FlightManager.Services.Interfaces;
using FlightManager.Web.Areas.Flights.Models.Flights;
using FlightManager.Web.Areas.Flights.Models.Passengers;
using FlightManager.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize]
    public class PassengersController : BaseController
    {
        private readonly IPassengersService passengersService;
        
        public PassengersController(IPassengersService passengersService)
        {
            this.passengersService = passengersService;
        }

        [Route("/Flights/{id}/Passengers")]
        public async Task<IActionResult> Index(int id)
        {
            var passengers = (await this.passengersService.GetAllForFlight(id))
                .Select(AutoMapperConfig.MapperInstance.Map<PassengerListingViewModel>)
                .ToArray();
            
            return this.View(passengers);
        }
    }
}