using System.Text.Json;

namespace BoatRazorLibrary.Models;

public class WeatherForecastService : IWeatherForcast
{
    private readonly IHttpClientFactory _clientFactory;

    public WeatherForecastService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    public async Task<ResponseWeatherModel> GetWeatherData()
    {
        ResponseWeatherModel result = new ResponseWeatherModel();

        var url = "http://api.openweathermap.org/data/2.5/weather?q=miami&appid=8ec38ece4bd75eaf3acb5330aaade5d0";
        //"https://api.openweathermap.org/data/3.0/onecall?lat=33.44&lon=-94.04&exclude=hourly,daily,minutely&appid=8ec38ece4bd75eaf3acb5330aaade5d0";
        //"http://api.openweathermap.org/data/2.5/forecast?id=524901&appid=8ec38ece4bd75eaf3acb5330aaade5d0";

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Accept", "application/json");

        var client = _clientFactory.CreateClient();

        var response = client.SendAsync(request).Result;

        if (response.IsSuccessStatusCode)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            try
            {
                result = JsonSerializer.Deserialize<ResponseWeatherModel>(stringResponse);

                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }
        return result;

    }
}