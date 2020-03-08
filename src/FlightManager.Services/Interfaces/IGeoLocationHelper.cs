using System.Threading.Tasks;

namespace FlightManager.Services.Interfaces
{
    public interface IGeoLocationHelper
    {
        string GetCityName(double latitude, double longitude);
    }
}