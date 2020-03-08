using System.Linq;
using System.Threading.Tasks;
using FlightManager.Common;
using FlightManager.Data;
using FlightManager.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FlightManager.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task InitializeDatabaseAsync(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<FlightManagerDbContext>();

                await dbContext.Database.MigrateAsync();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(GlobalConstants.AdministratorRoleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(GlobalConstants.AdministratorRoleName));
                }
                
                if (!await roleManager.RoleExistsAsync(GlobalConstants.EmployeeRoleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(GlobalConstants.EmployeeRoleName));
                }
            }
        }
    }
}