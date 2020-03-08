namespace FlightManager.Web.Controllers
{
    using FlightManager.Common;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    
    public abstract class BaseController : Controller
    {
        protected void Error(string message)
        {
            this.TempData[GlobalConstants.ErrorNotificationKey] = message;
        }

        protected void Success(string message)
        {
            this.TempData[GlobalConstants.SuccessNotificationKey] = message;
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}