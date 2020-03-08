namespace FlightManager.Web.Areas.Account.Models.Authentication
{
    using System.ComponentModel.DataAnnotations;
    
    public class UserLoginBindingModel
    {
        [Required(ErrorMessage = "Моля въведете валидно потребителско име")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Моля въведете парола")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомни ме?")]
        public bool RememberMe { get; set; }
    }
}