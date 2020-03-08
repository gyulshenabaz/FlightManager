namespace FlightManager.Models
{
    using System.ComponentModel.DataAnnotations;
    
    public class Passenger
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
        public TicketType TicketType { get; set; }
        
        [Required]
        public int ReservationId { get; set; }
        
        public Reservation Reservation { get; set; }
    }
}