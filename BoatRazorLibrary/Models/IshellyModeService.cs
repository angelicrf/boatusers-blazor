
namespace BoatRazorLibrary.Models;

public interface IshellyModeService
{
    Task<ShellyDeviceDataModel> ShellyDeviceChangeOrder(string thisId, string thisOrder);
}
