using System.Linq;

namespace FlightManager.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FlightManager.Common.AutoMapping.Interfaces;
    using FlightManager.Models;

    public class FlightServiceModel : IMappableFrom<Flight>, IMappableTo<Flight>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string From { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string To { get; set; }

        [Required] public DateTime DepartureDate { get; set; }

        [Required] public DateTime ArrivalDate { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string AircraftType { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string AircraftRegistration { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string PilotName { get; set; }

        [Required] [Range(5, 400)] public int AvailableSeats { get; set; }

        [Required] [Range(10, 400)] public int BusinessClassSeats { get; set; }

        public int EconomyClassSeats => AvailableSeats - BusinessClassSeats;

        public ICollection<ReservationServiceModel> Reservations { get; set; }
    }
}