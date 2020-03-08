using System;

namespace FlightManager.Data
{
    using FlightManager.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class FlightManagerDbContext : IdentityDbContext<FlightManagerUser>
    {
        public FlightManagerDbContext(DbContextOptions<FlightManagerDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Flight> Flights { get; set; }

        public DbSet<Passenger> Passengers { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Passenger>()
                .Property(p => p.TicketType)
                .HasConversion(
                    v => v.ToString(),
                    v => (TicketType)Enum.Parse(typeof(TicketType), v));
            
            base.OnModelCreating(builder);
        }
    }
}