using System.Collections.Generic;
using System.Threading.Tasks;
using FlightManager.Services.Models;

namespace FlightManager.Services.Interfaces
{
    public interface IPassengersService
    {
        Task<IEnumerable<PassengerServiceModel>> GetAllForFlight(int flightId);
    }
}