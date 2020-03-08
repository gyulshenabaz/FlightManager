using System.Linq;
using System.Threading.Tasks;
using FlightManager.Common;
using FlightManager.Common.AutoMapping;
using FlightManager.Common.EmailSender.Interfaces;
using FlightManager.Services.Interfaces;
using FlightManager.Services.Models;
using FlightManager.Web.Areas.Flights.Models.Flights;
using FlightManager.Web.Areas.Flights.Models.Reservations;
using FlightManager.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Areas.Flights.Controllers
{
    [Area("Flights")]
    public class ReservationsController : BaseController
    {
        private readonly IReservationsService reservationsService;
        private readonly IFlightsService flightsService;
        
        public ReservationsController(IReservationsService reservationsService,
            IFlightsService flightsService,
            IEmailSender emailSender)
        {
            this.reservationsService = reservationsService;
            this.flightsService = flightsService;
        }
        
        [HttpGet]
        [Route("/Flight/{id}/Reservation")]
        public async Task<IActionResult> Create(int id, string email)
        {
            var flightServiceModel = await this.flightsService.GetAsync(id);

            if (flightServiceModel == null || !(await this.flightsService.CanBookAsync(id)))
            {
                return this.RedirectToAction("Details", "Flights", new {id});
            }

            var model = new ReservationCreateBindingModel()
            {
                Email = email,
                FlightId = id,
                Flight = AutoMapperConfig.MapperInstance.Map<FlightDetailsViewModel>(flightServiceModel)
            };
            
            return this.View(model);
        }

        [HttpPost]
        [Route("/Flight/{id}/Reservation")]
        public async Task<IActionResult> Create(int id, [FromBody] ReservationCreateBindingModel model)
        {
            model.FlightId = id;

            if (!model.Passengers.Any() || !ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var reservationId = await this.reservationsService.CreateAsync(
                AutoMapperConfig.MapperInstance.Map<ReservationServiceModel>(model));

            if (reservationId == null)
            {
                this.Error(NotificationMessages.ReservationError);
                
                return Json(Url.Action("Details", "Flights", new {id=id}));
            }
            
            this.Success(NotificationMessages.ReservationSuccess);
            
            return Json(Url.Action("Details", "Reservations", new {id=reservationId}));
        }
        
        [Route("/Reservations/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var serviceModel = await this.reservationsService.GetAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }
            
            var model = AutoMapperConfig.MapperInstance.Map<ReservationDetailsViewModel>(serviceModel);

            return this.View(model);
        }
    }
} 