
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

    [JsonPropertyName("device_id")]
    public string? DeviceResponseId { get; set; }
}
public class ShellyDeviceStatus
{

    [JsonPropertyName("id")]
    public string? id { get; set; }

    [JsonPropertyName("switch:0")]
    public ShellyDeviceSwich? SwitchInfo { get; set; }
    //Lamp
    [JsonPropertyName("getinfo")]
    public ShellyDeviceLampGetInfo? GetInfo { get; set; }
    //lights
    [JsonPropertyName("lights")]
    public List<ShellyDeviceLightSystem>? LightSystem { get; set; }
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
public class ShellyDeviceLampGetInfo
{
    [JsonPropertyName("fw_info")]
    public ShellyDeviceLampGetInfoModel ShellyDeviceLampInfoModel { get; set; }
}
public class ShellyDeviceLampGetInfoModel
{
    [JsonPropertyName("device")]
    public string? LampDeviceId { get; set; }
}
public class ShellyDeviceLightSystem
{
    [JsonPropertyName("brightness")]
    public int? LampBrightness { get; set; }

    [JsonPropertyName("white")]
    public int? LampWhiteness { get; set; }
}