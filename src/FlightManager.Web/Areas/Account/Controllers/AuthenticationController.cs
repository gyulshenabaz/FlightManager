using System.Threading.Tasks;
using FlightManager.Common;
using FlightManager.Models;
using FlightManager.Web.Areas.Account.Models.Authentication;
using FlightManager.Web.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Areas.Account.Controllers
{
    [Area("Account")]
    public class AuthenticationController : BaseController
    {   
        private readonly SignInManager<FlightManagerUser> signInManager;

        public AuthenticationController(SignInManager<FlightManagerUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Account/Login")]
        public IActionResult Login(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (User.Identity.IsAuthenticated)
            {
                return this.LocalRedirect(returnUrl);
            }
            
            return this.View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("Account/Login")]
        public async Task<IActionResult> Login(UserLoginBindingModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (User.Identity.IsAuthenticated)
            {
                return this.LocalRedirect(returnUrl);
            }
            
            if (ModelState.IsValid)
            {
                var result = await signInManager
                    .PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return this.LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Невалидно потребителско име или парола");
                }
            }

            return this.View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            
            this.Success(NotificationMessages.SuccessfullyLoggedOut);
            
            return this.LocalRedirect("/");
        }
    }
}