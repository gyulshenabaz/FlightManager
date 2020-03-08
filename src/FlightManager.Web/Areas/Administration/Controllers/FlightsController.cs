using System.Linq;
using System.Threading.Tasks;
using FlightManager.Common;
using FlightManager.Common.AutoMapping;
using FlightManager.Services.Interfaces;
using FlightManager.Services.Models;
using FlightManager.Web.Areas.Administration.Models.Flights;
using FlightManager.Web.Areas.Flights.Models.Flights;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Areas.Administration.Controllers
{
    public class FlightsController : AdministrationController
    {
        private readonly IFlightsService flightsService;

        public FlightsController(IFlightsService flightsService)
        {
            this.flightsService = flightsService;
        }
        
        public async Task<IActionResult> Index()
        {
            var allFlights = await this.flightsService.GetAllAsync();

            return this.View(allFlights.Select(AutoMapperConfig.MapperInstance.Map<FlightListingViewModel>));
        }
        
        public IActionResult Create()
        {
            var model = new FlightCreateBindingModel();

            return this.View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(FlightCreateBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            
            var serviceModel = AutoMapperConfig.MapperInstance.Map<FlightServiceModel>(model);

            var id = await this.flightsService.CreateAsync(serviceModel);
            
            if (id == null)
            {
                this.Error(NotificationMessages.FlightCreateError);
                
                return this.View(model);
            }

            this.Success(NotificationMessages.FlightCreated);
            
            return this.RedirectToAction("Details", new {id, Area = "Flights"});
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var serviceModel = await this.flightsService.GetAsync(id);

            if (serviceModel == null || !(await this.flightsService.CanBookAsync(id)))
            {
                return this.NotFound();
            }

            var bindingModel = AutoMapperConfig.MapperInstance.Map<FlightEditBindingModel>(serviceModel);

            return this.View(bindingModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(int id, FlightEditBindingModel model)
        {
            var serviceModel = await this.flightsService.GetAsync(id);

            if (serviceModel == null || !(await this.flightsService.CanBookAsync(id)))
            {
                return this.NotFound();
            }
            
            serviceModel.Id = id;
            serviceModel.AircraftType = model.AircraftType;
            serviceModel.AircraftRegistration = model.AircraftRegistration;
            serviceModel.PilotName = model.PilotName;
            serviceModel.AvailableSeats = model.AvailableSeats;
            serviceModel.BusinessClassSeats = model.BusinessClassSeats;
            
            if (!this.ModelState.IsValid)
            {
                var bindingModel = AutoMapperConfig.MapperInstance.Map<FlightEditBindingModel>(serviceModel);

                return this.View(bindingModel);
            }

            var result = await this.flightsService.EditAsync(serviceModel);
            
            if (result)
            {
                this.Success(NotificationMessages.FlightEdited);
            }
            else
            {
                this.Error(NotificationMessages.FlightEditError);
            }

            return this.RedirectToAction("Details", new {id, Area = "Flights"});
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.flightsService.DeleteAsync(id);

            if (result)
            {
                this.Success(NotificationMessages.FlightDeleted);
            }
            else
            {
                this.Error(NotificationMessages.FlightDeleteError);
            }

            return this.RedirectToAction("Index");
        }
    }
}