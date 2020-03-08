using System.Threading.Tasks;
using FlightManager.Services.Models;

namespace FlightManager.Services.Interfaces
{
    public interface IReservationsService
    {
        Task<ReservationServiceModel> GetAsync(int id);
        
        Task<int?> CreateAsync(ReservationServiceModel model);

        Task<bool> DeleteAsync(int id);

    }
}