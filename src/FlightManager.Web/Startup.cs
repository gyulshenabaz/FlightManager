using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Abstractions;
using System.Reflection;
using FlightManager.Common.AutoMapping;
using FlightManager.Common.EmailSender;
using FlightManager.Common.EmailSender.Implementation;
using FlightManager.Common.EmailSender.Interfaces;
using FlightManager.Data;
using FlightManager.Data.Interfaces;
using FlightManager.Data.Repository;
using FlightManager.Models;
using FlightManager.Services.Implementations;
using FlightManager.Services.Implementations.Helpers;
using FlightManager.Services.Interfaces;
using FlightManager.Services.Models;
using FlightManager.Web.Areas.Administration.Models.Flights;
using FlightManager.Web.Areas.Administration.Models.Users;
using FlightManager.Web.Areas.Flights.Models.Flights;
using FlightManager.Web.Extensions;
using FlightManager.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FlightManager.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookieTempDataProviderOptions>(options => { options.Cookie.IsEssential = true; });
            
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Error403";
                options.Cookie.HttpOnly = true;
            });
            
            services.AddDbContext<FlightManagerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddIdentity<FlightManagerUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<FlightManagerDbContext>()
                .AddDefaultTokenProviders();
            
            services.Configure<SendGridConfiguration>(Configuration.GetSection("SendGridConfiguration"));
            
            services.AddControllersWithViews();
            
            services.AddTransient<IEmailSender, EmailSender>();
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IFlightsService, FlightsService>();
            services.AddScoped<IReservationsService, ReservationsService>();
            services.AddScoped<IPassengersService, PassengersService>();

            services.AddSingleton<IFileSystem, FileSystem>();
            services.AddSingleton<ICountriesHelper, CountriesHelper>();
            services.AddSingleton<IGeoLocationHelper, GeoLocationHelper>();
            
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(FlightServiceModel).GetTypeInfo().Assembly, typeof(Flight).GetTypeInfo().Assembly,
                typeof(FlightCreateBindingModel).GetTypeInfo().Assembly);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.InitializeDatabaseAsync().GetAwaiter().GetResult();

            var supportedCultures = new[]
            {
                new CultureInfo("bg-BG"),
                new CultureInfo("bg-BG"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("bg-BG"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseStatusCodePages();
            app.UseStatusCodePagesWithReExecute("/Error{0}");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}