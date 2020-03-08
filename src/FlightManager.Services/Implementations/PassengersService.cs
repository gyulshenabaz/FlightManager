using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Common.AutoMapping.Extensions;
using FlightManager.Data.Interfaces;
using FlightManager.Models;
using FlightManager.Services.Interfaces;
using FlightManager.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Services.Implementations
{
    public class PassengersService : BaseService, IPassengersService
    {
        private readonly IRepository<Passenger> passengersRepository;
        private readonly IRepository<Flight> flightsRepository;
        
        public PassengersService(IRepository<Passenger> passengersRepository,
            IRepository<Flight> flightsRepository)
        {
            this.passengersRepository = passengersRepository;
            this.flightsRepository = flightsRepository;
        }
        
        public async Task<IEnumerable<PassengerServiceModel>> GetAllForFlight(int flightId)
        {
            var flight = await this.flightsRepository.All()
                .SingleOrDefaultAsync(f => f.Id == flightId);

            if (flight == null)
            {
                return null;
            }
            
            var passengers = await this.passengersRepository.All()
                .Include(p=> p.Reservation)
                .Where(p=> p.Reservation.FlightId == flightId)
                .To<PassengerServiceModel>()
                .ToArrayAsync();

            return passengers;
        }
    }
}