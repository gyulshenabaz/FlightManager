namespace FlightManager.Services.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FlightManager.Common.AutoMapping.Interfaces;
    using FlightManager.Models;
    
    public class ReservationServiceModel : IMappableFrom<Reservation>, IMappableTo<Reservation>
    {
        public int Id { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public int FlightId { get; set; }
        
        public FlightServiceModel Flight { get; set; }
        
        public ICollection<PassengerServiceModel> Passengers { get; set; }
    }
}