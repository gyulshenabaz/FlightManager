using System.Threading.Tasks;
using FlightManager.Common;
using FlightManager.Common.AutoMapping;
using FlightManager.Common.AutoMapping.Extensions;
using FlightManager.Models;
using FlightManager.Web.Areas.Administration.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Web.Areas.Administration.Controllers
{
    public class UsersController : AdministrationController
    {
        private readonly UserManager<FlightManagerUser> userManager;

        public UsersController(UserManager<FlightManagerUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await this.userManager.Users
                .To<UserAdminListingModel>()
                .ToArrayAsync();

            return this.View(users);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var model = new UserCreateBindingModel();
            
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = new FlightManagerUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email,
                UniqueCitizenshipNumber = model.UniqueCitizenshipNumber,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };
            
            var result = await this.userManager.CreateAsync(user, model.Password);

            var addRole =
                await this.userManager.AddToRoleAsync(await userManager.FindByNameAsync(user.UserName),
                    GlobalConstants.EmployeeRoleName);
            
            if (result.Succeeded && addRole.Succeeded)
            {
                this.Success(NotificationMessages.UserAccountCreated);
                
                return this.RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }
            
            this.Error(NotificationMessages.UserAccountCreatError);
            
            return this.View(model);
        }

        public async Task<IActionResult> Edit(string username)
        {
            if (username == null)
            {
                return this.NotFound();
            }

            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
                
            {
                return NotFound();
            }

            var bindingModel = new UserEditBindingModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                UniqueCitizenshipNumber = user.UniqueCitizenshipNumber,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };

            return this.View(bindingModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(string username, UserEditBindingModel model)
        {
            if (username == null)
            {
                return this.NotFound();
            }

            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.Username;
            user.Email = model.Email;
            user.UniqueCitizenshipNumber = model.UniqueCitizenshipNumber;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;

            var result = await userManager.UpdateAsync(user);
            
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            if (result.Succeeded)
            {
                this.Success(NotificationMessages.UserAccountEdited);
                
                return this.RedirectToAction("Index");
            }
            
            this.Error(NotificationMessages.UserAccountEditError);
            
            return this.View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(string password, string id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            var passwordIsValid = !await this.userManager.HasPasswordAsync(currentUser) ||
                                  password != null &&
                                  await this.userManager.CheckPasswordAsync(currentUser, password);

            if (!passwordIsValid)
            {
                this.Error(NotificationMessages.PasswordIncorrect);
                
                return this.RedirectToAction("Index");
            }

            var user = await this.userManager.FindByIdAsync(id);

            if (user == null ||
                await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName))
            {
                this.Error("");
                return this.RedirectToAction("Index");
            }

            var result = await this.userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                this.Success(NotificationMessages.UserAccountDeleted);
            }
            
            this.Error(NotificationMessages.UserAccountDeleteError);
            
            return this.RedirectToAction("Index");
        }
        
        public async Task<IActionResult> ChangePassword(string username)
        {
            if (username == null)
            {
                return this.NotFound();
            }

            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return this.NotFound();
            }

            var bindingModel = AutoMapperConfig.MapperInstance.Map<UserChangePasswordBindingModel>(user);

            return this.View(bindingModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string username, UserChangePasswordBindingModel model)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            
            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);

            var result = await this.userManager.ResetPasswordAsync(user, token, model.Password);

            if (!result.Succeeded)
            {
                this.Error(NotificationMessages.UserChangePasswordError);
                
                return this.RedirectToAction("Index");
            }

            this.Success(NotificationMessages.UserSuccessfullyChangedPassword);
            
            return this.RedirectToAction("Index");
        }
    }
}