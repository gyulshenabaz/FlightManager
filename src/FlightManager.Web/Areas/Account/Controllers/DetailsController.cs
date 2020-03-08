using System.Threading.Tasks;
using FlightManager.Common;
using FlightManager.Models;
using FlightManager.Web.Areas.Account.Models.Details;
using FlightManager.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Areas.Account.Controllers
{
    [Authorize]
    [Area("Account")]
    public class DetailsController : BaseController
    {
        private readonly UserManager<FlightManagerUser> userManager;

        public DetailsController(UserManager<FlightManagerUser> userManager)
        {
            this.userManager = userManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var model = new UserEditBindingModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UniqueCitizenshipNumber =  user.UniqueCitizenshipNumber,
                Address = user.Address
            };

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(UserEditBindingModel model)
        {
            var user = await this.userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }
                        
            if (!ModelState.IsValid)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.UniqueCitizenshipNumber = user.UniqueCitizenshipNumber;

                return this.View(model);
            }
            
            user.UserName = model.Username;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;

            var result = await this.userManager.UpdateAsync(user);

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
                
                return this.View(model);
            }
            
            this.Success(NotificationMessages.UserAccountUpdated);
            
            return this.LocalRedirect("/");
        }
        
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await this.userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }
            
            var model = new UserChangePasswordBindingModel();
            
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var changePasswordResult =
                await this.userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }

                return this.View(model);
            }

            this.Success(NotificationMessages.UserSuccessfullyChangedPassword);
            
            return this.LocalRedirect("/");
        }
    }
}