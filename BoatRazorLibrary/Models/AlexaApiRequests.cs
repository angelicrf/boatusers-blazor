using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace BoatRazorLibrary.Models;

public class AlexaApiRequests
{
    private readonly HttpClient _httpClient;
    private string ThisUri = "https://76ewqh4kz26525z22jjuyzooqy0ziulc.lambda-url.us-east-1.on.aws/?alexaMsg=PowerController";
    private object[] getProperties { get; set; }
    public AlexaApiRequests()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(ThisUri);
    }
    public async Task<object[]> PostAlexaAPIPowerControlEvent(string thisAccessToken)
    {
        AlexaAPIDataModel result = new AlexaAPIDataModel();

        try
        {
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();

            string payload = JsonConvert.SerializeObject(new
            {
                powerHeader = new
                {
                    namespacedm = "Alexa.PowerController",
                    name = "TurnOff",
                    messageId = myuuidAsString
                },

                powerToken = $"{thisAccessToken}"
            });
            //x-www-form-urlencoded
            using (HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json"))
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
                            var newResult = resultContent.Result;
                            result = System.Text.Json.JsonSerializer.Deserialize<AlexaAPIDataModel>(resultContent.Result, options);
                            var getDeviceData = result.DeviceData;
                            getProperties = getDeviceData.PRproperties;
                            for (int i = 0; i < getProperties.Length; i++)
                            {
                                Console.WriteLine($"all properties : {getProperties[i]}");
                            }

                        }
                }
            }
        }
        catch (Exception env)
        {

            Console.WriteLine(env.Message);
        }
        return getProperties;
    }

}
