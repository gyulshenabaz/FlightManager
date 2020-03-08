namespace FlightManager.Services.Models
{
    using System.ComponentModel.DataAnnotations;
    using FlightManager.Common.AutoMapping.Interfaces;
    using FlightManager.Models;
    using Microsoft.AspNetCore.Identity;
    
    public class FlightManagerUserServiceModel : IdentityUser, IMappableFrom<FlightManagerUser>
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