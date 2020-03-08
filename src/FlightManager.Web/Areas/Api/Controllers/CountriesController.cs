using System.IO.Abstractions;
using FlightManager.Services.Interfaces;
using FlightManager.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlightManager.Web.Areas.Api.Controllers
{
    public class CountriesController : ApiController
    {
        private readonly ICountriesHelper countriesHelper;

        public CountriesController(ICountriesHelper countriesHelper)
        {
            this.countriesHelper = countriesHelper;
        }
        
        public IActionResult GetAll()
        {
            return new JsonResult(countriesHelper.GetAll());
        }
    }
}