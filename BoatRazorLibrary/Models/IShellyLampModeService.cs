
namespace BoatRazorLibrary.Models;

public interface IShellyLampModeService
{
    Task<ShellyDeviceDataModel> ShellyDeviceChangeOrder(string thisId, string orderSt);
}
