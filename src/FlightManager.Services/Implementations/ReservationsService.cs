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
    public class ReservationsService : BaseService, IReservationsService
    {
        private readonly IRepository<Flight> flightsRepository;
        private readonly IRepository<Reservation> reservationsRepository;
        private readonly IEmailSender emailSender;

        public ReservationsService(IRepository<Flight> flightsRepository,
            IRepository<Reservation> reservationsRepository,
            IEmailSender emailSender)
        {
            this.flightsRepository = flightsRepository;
            this.reservationsRepository = reservationsRepository;
            this.emailSender = emailSender;
        }
        
        public async Task<int?> CreateAsync(ReservationServiceModel model)
        {
            if (!this.IsEntityStateValid(model))
            {
                return null;
            }

            var flight = await this.flightsRepository.All()
                .SingleOrDefaultAsync(f => f.Id == model.FlightId);

            if (flight == null)
            {
                return null;
            }

            var passengers = await this.GetPassengersAsync(flight.Id, model.Passengers);

            if (!passengers.Any())
            {
                return null;
            }

            model.Passengers = passengers;
            
            var reservation = AutoMapperConfig.MapperInstance.Map<Reservation>(model);

            await this.reservationsRepository.AddAsync(reservation);

            await this.reservationsRepository.SaveChangesAsync();

            await this.emailSender.SendEmailAsync(reservation.Email, SendGridTemplates.BookingConfirmation,
                AutoMapperConfig.MapperInstance.Map<ReservationEmailViewModel>(reservation));

            return reservation.Id;
        }

        private async Task<ICollection<PassengerServiceModel>> GetPassengersAsync(int flightId,
            ICollection<PassengerServiceModel> serviceModels)
        {
            List<PassengerServiceModel> passenger = new List<PassengerServiceModel>();
            
            var passengersInBusinessClass = await GetPassengersForBusinessClassAsync(flightId, serviceModels);
            var passengersInEconomyClass = await GetPassengersForEconomyClassAsync(flightId, serviceModels);

            if (passengersInBusinessClass != null)
            {
                passenger.AddRange(passengersInBusinessClass);
            }
            
            if (passengersInEconomyClass != null)
            {
                passenger.AddRange(passengersInEconomyClass);
            }
            
            return passenger;
        }
        
        public async Task<ReservationServiceModel> GetAsync(int id)
        {
            var reservation = await this.reservationsRepository.All()
                .To<ReservationServiceModel>()
                .SingleOrDefaultAsync(r => r.Id == id);

            return reservation;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var reservation = await this.reservationsRepository.All()
                .Include(f=>f.Flight)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return false;
            }

            await this.emailSender.SendEmailAsync(reservation.Email, SendGridTemplates.CancelledReservation,
                AutoMapperConfig.MapperInstance.Map<ReservationEmailViewModel>(reservation));
            
            this.reservationsRepository.Remove(reservation);

            await this.flightsRepository.SaveChangesAsync();

            return true;
        }
        
        private async Task<ICollection<PassengerServiceModel>> GetPassengersForBusinessClassAsync(int flightId,
            ICollection<PassengerServiceModel> serviceModels)
        {
            var flight = await this.flightsRepository.All()
                .SingleOrDefaultAsync(f => f.Id == flightId);
            
            if (flight == null)
            {
                return null;
            }

            var passengers = serviceModels
                .Where(p => p.TicketType == TicketType.Бизнес.ToString())
                .ToList();
            
            var passengersInBusinessClass = await this.GetAllPassengersInBusinessClassAsync(flightId);

            var areTaken = flight.BusinessClassSeats - (passengersInBusinessClass + passengers.Count) < 0;

            return areTaken ? null : passengers;
        }
        
        private async Task<ICollection<PassengerServiceModel>> GetPassengersForEconomyClassAsync(int flightId,
            ICollection<PassengerServiceModel> serviceModels)
        {
            var flight = await this.flightsRepository.All()
                .SingleOrDefaultAsync(f => f.Id == flightId);
            
            if (flight == null)
            {
                return null;
            }

            var passengers = serviceModels
                .Where(p => p.TicketType == TicketType.Икономична.ToString())
                .ToList();
            
            var passengersInEconomyClass = await this.GetAllPassengersInEconomyClassAsync(flightId);

            var areTaken = flight.EconomyClassSeats - (passengersInEconomyClass + passengers.Count) < 0;

            return areTaken ? null : passengers;
        }
        
        private async Task<int> GetAllPassengersInBusinessClassAsync(int flightId)
        {
            var passengers = await this.reservationsRepository.All()
                .Where(r=>r.FlightId == flightId)
                .Include(r => r.Passengers)
                .Select(r => r.Passengers.Where(p => p.TicketType == TicketType.Бизнес))
                .CountAsync();

            return passengers;
        }
        
        private async Task<int> GetAllPassengersInEconomyClassAsync(int flightId)
        {
            var passengers = await this.reservationsRepository.All()
                .Where(r=>r.FlightId == flightId)
                .Include(r => r.Passengers)
                .Select(r =>r.Passengers.Where(p => p.TicketType == TicketType.Икономична))
                .CountAsync();

            return passengers;
        }
    }
}