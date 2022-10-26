using System.Text.Json.Serialization;

namespace BoatRazorLibrary.Models;

public class DeviceLocationW
{
    [JsonPropertyName("lat")]
    public float LatCoordinate { get; set; }

    [JsonPropertyName("lon")]
    public float LonCoordinate { get; set; }

    [JsonPropertyName("deviceLoc")]
    public string DeviceAddress { get; set; }
}
