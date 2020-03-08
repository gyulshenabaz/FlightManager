using System.Threading.Tasks;
using FlightManager.Common;
using FlightManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Areas.Administration.Controllers
{
    public class ReservationsController : AdministrationController
    {
        private readonly IReservationsService reservationsService;
        
        public ReservationsController(IReservationsService reservationsService)
        {
            this.reservationsService = reservationsService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.reservationsService.DeleteAsync(id);

            if (result)
            {
                this.Success(NotificationMessages.ReservationDeleted);
            }
            else
            {
                this.Error(NotificationMessages.ReservationDeleteError);
            }

            return this.LocalRedirect("/");
        }
    }
}