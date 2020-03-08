namespace FlightManager.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class Reservation
    {
        public int Id { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public int FlightId { get; set; }
        
        public Flight Flight { get; set; }
        
        public ICollection<Passenger> Passengers { get; set; }
    }
}