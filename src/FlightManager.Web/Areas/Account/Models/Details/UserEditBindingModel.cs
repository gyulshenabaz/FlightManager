using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.Areas.Account.Models.Details
{
    public class UserEditBindingModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Моля въведете потребителско име")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Моля въведете имейл адрес")]
        [EmailAddress(ErrorMessage = "Невалиден имейл адрес")]
        public string Email { get; set; }
        public string UniqueCitizenshipNumber { get; set; }
        
        [Required(ErrorMessage = "Моля въведете телефонен номер")]
        [Phone(ErrorMessage = "Невалиден телефонен номер")]
        public string PhoneNumber { get; set; }
        
        public string Address { get; set; }
    }
}