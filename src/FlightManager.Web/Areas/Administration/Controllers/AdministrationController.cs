using FlightManager.Common;
using FlightManager.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public abstract class AdministrationController : BaseController
    {
    }
}