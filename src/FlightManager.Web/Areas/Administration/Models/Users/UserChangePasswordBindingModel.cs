using System.ComponentModel.DataAnnotations;
using FlightManager.Common.AutoMapping.Interfaces;
using FlightManager.Models;

namespace FlightManager.Web.Areas.Administration.Models.Users
{
    public class UserChangePasswordBindingModel : IMappableFrom<FlightManagerUser>
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Въведете парола")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Потвърдете парола")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат")]
        public string ConfirmNewPassword { get; set; }
    }
}