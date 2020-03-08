using System.ComponentModel.DataAnnotations;

namespace FlightManager.Models
{
    using Microsoft.AspNetCore.Identity;
    
    public class FlightManagerUser : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        
        [Required]
        [RegularExpression(@"^\d{10}$")]
        public string UniqueCitizenshipNumber { get; set; }
        
        public string Address { get; set; }
    }
}