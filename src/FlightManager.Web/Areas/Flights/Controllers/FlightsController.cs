using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlightManager.Common;
using FlightManager.Common.AutoMapping;
using FlightManager.Services.Interfaces;
using FlightManager.Services.Models;
using FlightManager.Web.Areas.Flights.Models.Flights;
using FlightManager.Web.Areas.Flights.Models.Reservations;
using FlightManager.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Areas.Flights.Controllers
{
    [Route("Flights/[action]/{id?}")]
    [Area("Flights")]
    [AllowAnonymous]
    public class FlightsController : BaseController
    {
        private readonly IFlightsService flightsService;

        public FlightsController(IFlightsService flightsService)
        {
            this.flightsService = flightsService;
        }
        
        [Route("/Flights/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var serviceModel = await this.flightsService.GetAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }
            
            var model = AutoMapperConfig.MapperInstance.Map<FlightDetailsViewModel>(serviceModel);

            return this.View(model);
        }
        
        public async Task<IActionResult> Search(SearchBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var flights = (await this.flightsService
                    .GetAllUpcomingWithFreeSeatsAsync())
                .Where(f => string.Equals(f.From, model.From, StringComparison.OrdinalIgnoreCase)  && 
                            string.Equals(f.To, model.To, StringComparison.OrdinalIgnoreCase) &&
                            f.DepartureDate.Date == model.Date)
                .Select(AutoMapperConfig.MapperInstance.Map<FlightListingViewModel>);

            model.FoundFlights = flights;

            return this.View(model);
        }
        
        [Route("/browse")]
        public async Task<IActionResult> All()
        {
            var rides = (await this.flightsService
                    .GetAllUpcomingWithFreeSeatsAsync())
                .Select(AutoMapperConfig.MapperInstance.Map<FlightListingViewModel>)
                .ToArray();

            return this.View(rides);
        }
    }
}