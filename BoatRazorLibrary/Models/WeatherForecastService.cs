using Newtonsoft.Json;

namespace BoatRazorLibrary.Models;

public class WeatherForecastService : IWeatherForcast
{
    private readonly HttpClient _httpClient;

    public WeatherForecastService(HttpClient client)
    {
        _httpClient = client;
    }
    public async Task<ResponseWeatherModel> GetWeatherData()
    {
        ResponseWeatherModel result = new ResponseWeatherModel();
        //_httpClient = new HttpClient();
        var url = "http://api.openweathermap.org/data/2.5/weather?q=miami&appid=8ec38ece4bd75eaf3acb5330aaade5d0";
        //"https://api.openweathermap.org/data/3.0/onecall?lat=33.44&lon=-94.04&exclude=hourly,daily,minutely&appid=8ec38ece4bd75eaf3acb5330aaade5d0";
        //"http://api.openweathermap.org/data/2.5/forecast?id=524901&appid=8ec38ece4bd75eaf3acb5330aaade5d0";
        try
        {
            using (Stream s = _httpClient.GetStreamAsync(url).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader read = new JsonTextReader(sr))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();

                try
                {
                    result = serializer.Deserialize<ResponseWeatherModel>(read);

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
        }
        catch (Exception env)
        {

            throw new Exception(env.Message);
        }

        return result;

    }
}