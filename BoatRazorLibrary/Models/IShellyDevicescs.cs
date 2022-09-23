
namespace BoatRazorLibrary.Models;

public interface IShellyDevicescs
{
    Task<ShellyDeviceDataModel> GetShellyDeviceStatus();
}
