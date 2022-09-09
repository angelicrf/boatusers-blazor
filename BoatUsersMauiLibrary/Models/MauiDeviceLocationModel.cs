

using Newtonsoft.Json;

namespace BoatUsersMauiLibrary.Models;

public class MauiDeviceLocationModel
{
    [JsonProperty("Latitude")]
    public double Latitude { get; set; } = 0.2;
    [JsonProperty("Longitude")]
    public double Longitude { get; set; } = 0.5;
}
