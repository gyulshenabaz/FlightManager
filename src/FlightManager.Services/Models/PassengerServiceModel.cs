namespace FlightManager.Services.Models
{
    using System.ComponentModel.DataAnnotations;
    using FlightManager.Common.AutoMapping.Interfaces;
    using FlightManager.Models;
    
    public class PassengerServiceModel : IMappableFrom<Passenger>, IMappableTo<Passenger>
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string MiddleName { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        
        [Required]
        [RegularExpression(@"^\d{10}$")]
        public string UniqueCitizenshipNumber { get; set; }
        
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Nationality { get; set; }
        
        [Required]
        public string TicketType { get; set; }
        
        [Required]
        public int ReservationId { get; set; }
        
        public ReservationServiceModel Reservation { get; set; }
    }
}