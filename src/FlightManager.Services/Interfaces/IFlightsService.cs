using System.Collections.Generic;
using System.Threading.Tasks;
using FlightManager.Services.Models;

namespace FlightManager.Services.Interfaces
{
    public interface IFlightsService
    {
        Task<int?> CreateAsync(FlightServiceModel model);
        
        Task<FlightServiceModel> GetAsync(int id);
        
        Task<IEnumerable<FlightServiceModel>> GetAllUpcomingWithFreeSeatsAsync();
        
        Task<bool> EditAsync(FlightServiceModel model);
        
        Task<IEnumerable<FlightServiceModel>> GetAllAsync();
        
        Task<bool> DeleteAsync(int id);

        Task<bool> CanBookAsync(int id);

        Task<bool> CanEditAsync(int id);
    }
}