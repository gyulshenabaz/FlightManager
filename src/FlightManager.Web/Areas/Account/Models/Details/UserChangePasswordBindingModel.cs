using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.Areas.Account.Models.Details
{
    public class UserChangePasswordBindingModel
    {
        [Required(ErrorMessage = "Въведете старата си парола")]
        [DataType(DataType.Password)]
        [Display(Name = "Стара парола")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Въведете новата си парола")]
        [DataType(DataType.Password)]
        [Display(Name = "Нова парола")]
        public string NewPassword { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Потвърдете новата парола")]
        [Compare("NewPassword", ErrorMessage = "Паролите не съвпадат")]
        public string ConfirmNewPassword { get; set; }
    }
}