
using System.Text.Json.Serialization;

namespace BoatRazorLibrary.Models;

public class ShellyDeviceDataModel
{
    [JsonPropertyName("data")]
    public ShellyDeviceDataInfo? DeviceData { get; set; }
}
public class ShellyDeviceDataInfo
{
    [JsonPropertyName("device_status")]
    public ShellyDeviceStatus? DeviceStatus { get; set; }
}
public class ShellyDeviceStatus
{

    [JsonPropertyName("id")]
    public string? id { get; set; }

    [JsonPropertyName("switch:0")]
    public ShellyDeviceSwich? SwitchInfo { get; set; }
}
public class ShellyDeviceSwich
{
    [JsonPropertyName("id")]
    public int? id { get; set; }

    [JsonPropertyName("temperature")]
    public ShellyDeviceTempreture? TemperatureDevice { get; set; }
}
public class ShellyDeviceTempreture
{
    [JsonPropertyName("tC")]
    public float? TemperatureDeviceCelcius { get; set; }

    [JsonPropertyName("tF")]
    public float? TemperatureDeviceFarenheit { get; set; }
}