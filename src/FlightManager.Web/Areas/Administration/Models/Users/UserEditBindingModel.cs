using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.Areas.Administration.Models.Users
{
    public class UserEditBindingModel
    {
        public string Id { get; set; }        
        
        [Required(ErrorMessage = "Моля въведете име")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Вашето име трябва да съдържа минимум 2 букви")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Моля въведете фамилно име")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Вашето име трябва да съдържа минимум 2 букви")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Моля въведете потребителско име")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Моля въведете имейл адрес")]
        [EmailAddress(ErrorMessage = "Невалиден имейл адрес")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Моля въведете ЕГН")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Невалидно ЕГН")]
        public string UniqueCitizenshipNumber { get; set; }
        
        [Required(ErrorMessage = "Моля въведете телефонен номер")]
        [Phone(ErrorMessage = "Невалиден телефонен номер")]
        public string PhoneNumber { get; set; }
        
        public string Address { get; set; }
    }
}