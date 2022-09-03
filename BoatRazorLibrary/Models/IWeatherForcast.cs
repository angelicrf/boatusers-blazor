
namespace BoatRazorLibrary.Models;

public interface IWeatherForcast
{
    Task<ResponseWeatherModel> GetWeatherData();

}
