using System.Text;
using System.Text.Json;

namespace BoatRazorLibrary.Models;

public class ShellyDevicesData : IShellyDevicescs, IDisposable
{
    private static readonly HttpClient _httpClient;

    private static string thisUri = "https://shelly-48-eu.shelly.cloud/device/status";
    static ShellyDevicesData()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(thisUri);
    }
    public async Task<ShellyDeviceDataModel> GetShellyDeviceStatus(string thisId)
    {

        ShellyDeviceDataModel result = new ShellyDeviceDataModel();
        //channel=0&turn=on&id=083AF200732C
        try
        {

            using (HttpContent content = new StringContent($"id={thisId}&auth_key=MTI1ZTUxdWlk313324368034E660810695C659D04F94E5270FA0EBF9E26F8FD9E027EC4310CE61A996667BB70DE8", Encoding.UTF8, "application/x-www-form-urlencoded"))
            using (HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = _httpClient.BaseAddress,
                Content = content
            })
            using (HttpResponseMessage response = await _httpClient.SendAsync(request))
            {

                if (response.IsSuccessStatusCode)
                {
                    using (var resultContent = response.Content.ReadAsStringAsync())

                        if (resultContent.Result != null)
                        {
                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true

                            };
                            result = System.Text.Json.JsonSerializer.Deserialize<ShellyDeviceDataModel>(resultContent.Result, options);

                            //Console.WriteLine(result.DeviceData.DeviceStatus.GetInfo.ShellyDeviceLampInfoModel.LampDeviceId);

                        }
                }
            }
        }
        catch (Exception env)
        {

            Console.WriteLine(env.Message);
        }
        return result;
    }
    public void Dispose()
    {
        _httpClient?.Dispose();
    }

}

