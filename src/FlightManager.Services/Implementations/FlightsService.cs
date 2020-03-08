using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Common;
using FlightManager.Common.AutoMapping;
using FlightManager.Common.AutoMapping.Extensions;
using FlightManager.Common.EmailSender.Interfaces;
using FlightManager.Data.Interfaces;
using FlightManager.Models;
using FlightManager.Services.Interfaces;
using FlightManager.Services.Models;
using FlightManager.Services.Models.Email;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Services.Implementations
{
    public class FlightsService : BaseService, IFlightsService
    {
        private readonly IRepository<Flight> flightsRepository;
        private readonly IRepository<Reservation> reservationsRepository;

        private readonly IEmailSender emailSender;

        public FlightsService(IRepository<Flight> flightsRepository,
            IRepository<Reservation> reservationsRepository,
            IEmailSender emailSender)
        {
            this.flightsRepository = flightsRepository;
            this.reservationsRepository = reservationsRepository;
            this.emailSender = emailSender;
        }
        
        public async Task<int?> CreateAsync(FlightServiceModel model)
        {
            if (!this.IsEntityStateValid(model))
            {
                return null;
            }
            
            var flight = AutoMapperConfig.MapperInstance.Map<Flight>(model);

            flight.Reservations = new List<Reservation>();

            await this.flightsRepository.AddAsync(flight);

            await this.flightsRepository.SaveChangesAsync();

            return flight.Id;
        }
        
        public async Task<IEnumerable<FlightServiceModel>> GetAllUpcomingWithFreeSeatsAsync()
        {
            var flights = await this.flightsRepository.All()
                .Where(f=>f.DepartureDate > DateTime.Now)
                .OrderBy(f => f.DepartureDate)
                .To<FlightServiceModel>()
                .ToArrayAsync();

            return flights;
        }
        
        public async Task<FlightServiceModel> GetAsync(int id)
        {
            var flight = await this.flightsRepository.All()
                .To<FlightServiceModel>()
                .SingleOrDefaultAsync(f => f.Id == id);

            return flight;
        }
        
        public async Task<bool> EditAsync(FlightServiceModel model)
        {
            if (!this.IsEntityStateValid(model))
            {
                return false;
            }

            var flight = await this.flightsRepository.All()
                .SingleOrDefaultAsync(f => f.Id == model.Id);

            if (flight == null)
            {
                return false;
            }

            flight.DepartureDate = model.DepartureDate;
            flight.ArrivalDate = model.ArrivalDate;
            flight.AircraftType = model.AircraftType;
            flight.AircraftRegistration = model.AircraftRegistration;
            flight.PilotName = model.PilotName;
            flight.AvailableSeats = model.AvailableSeats;
            flight.BusinessClassSeats = model.AvailableSeats;

            this.flightsRepository.Update(flight);
            
            await this.flightsRepository.SaveChangesAsync();

            return true;
        }
        
        public async Task<IEnumerable<FlightServiceModel>> GetAllAsync()
        {
            var flights = await this.flightsRepository.All()
                .To<FlightServiceModel>()
                .ToArrayAsync();

            return flights;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var flight = await this.flightsRepository.All()
                .Include(f=>f.Reservations)
                .SingleOrDefaultAsync(f => f.Id == id);

            if (flight == null)
            {
                return false;
            }

            this.flightsRepository.Remove(flight);

            this.reservationsRepository.RemoveRange(flight.Reservations);

            await this.emailSender.SendEmailToMultipleAsync(flight.Reservations.Select(r => r.Email).Distinct().ToList(), 
                SendGridTemplates.CancelledFlight,
                AutoMapperConfig.MapperInstance.Map<FlightCancelledEmailViewModel>(flight));

            await this.flightsRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CanBookAsync(int id)
        {
            var flight = await this.flightsRepository.All()
                .Include(f=>f.Reservations)
                .ThenInclude(r=>r.Passengers)
                .SingleOrDefaultAsync(f => f.Id == id);

            if (flight == null)
            {
                return false;
            }

            int bookedReservations = await this.reservationsRepository.All()
                .Include(r => r.Passengers)
                .Select(p => p.Passengers)
                .CountAsync();

            if (bookedReservations >= flight.AvailableSeats || flight.DepartureDate < DateTime.Now)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CanEditAsync(int id)
        {
            var flight = await this.flightsRepository.All()
                .SingleOrDefaultAsync(f => f.Id == id);

            if (flight == null || flight.DepartureDate < DateTime.Now)
            {
                return false;
            }

            return true;
        }
    }
}