using System.Text.Json.Serialization;

namespace BoatRazorLibrary.Models;

public class AlexaAPIDataModel
{
    [JsonPropertyName("context")]
    public AlexaPowerControlResponse DeviceData { get; set; }
}
public class AlexaPowerControlResponse
{
    [JsonPropertyName("properties")]
    public object[] PRproperties { get; set; }
}
