using Newtonsoft.Json;

namespace BoatRazorLibrary.Models;

public class ResponseWeatherModel
{
    [JsonProperty("name")]
    public string? name { get; set; }

    [JsonProperty("main")]
    public MainModel? main { get; set; }

    [JsonProperty("wind")]
    public WindModel? wind { get; set; }

    [JsonProperty("weather")]
    public List<WeatherModel>? weather { get; set; }
}

public class MainModel
{
    [JsonProperty("temp")]
    public float temp { get; set; }

    [JsonProperty("humidity")]
    public float humidity { get; set; }

    [JsonProperty("pressure")]
    public float pressure { get; set; }

}
public class WindModel
{
    [JsonProperty("speed")]
    public float speed { get; set; }
}
public class WeatherModel
{
    [JsonProperty("description")]
    public string? description { get; set; }
}
