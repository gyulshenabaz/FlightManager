using System.Threading.Tasks;
using FlightManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Areas.Api.Controllers
{
    public class LocationController : ApiController
    {
        private readonly IGeoLocationHelper geoLocationHelper;
        
        public LocationController(IGeoLocationHelper locationHelper)
        {
            this.geoLocationHelper = locationHelper;
        }
        
        public IActionResult GetCity(double latitude, double longitude)
        {
            var cityName =  this.geoLocationHelper.GetCityName(latitude, longitude);

            return new JsonResult(new { cityName });
        }
    }
}