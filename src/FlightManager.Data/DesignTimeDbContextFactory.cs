namespace FlightManager.Data
{
    using System.IO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FlightManagerDbContext>
    {
        public FlightManagerDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<FlightManagerDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new FlightManagerDbContext(builder.Options);
        }
    }
}