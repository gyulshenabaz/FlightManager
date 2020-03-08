using System.Collections.Generic;
using FlightManager.Services.Implementations.Helpers;

namespace FlightManager.Services.Interfaces
{
    public interface ICountriesHelper
    {
        ICollection<CountriesHelper.Country> GetAll();
    }
}