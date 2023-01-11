using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace BoatRazorLibrary.Models;

public class AlexaApiRequests
{
    private readonly HttpClient _httpClient;
    private string ThisUri = "https://76ewqh4kz26525z22jjuyzooqy0ziulc.lambda-url.us-east-1.on.aws/?alexaMsg=PowerController";
    public object[] GetProperties { get; set; }
    private string GetAToken { get; set; }
    public AlexaApiRequests()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(ThisUri);
    }
    public AlexaApiRequests(string setUrl)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(setUrl);
    }
    public async Task<object[]> PostAlexaAPIPowerControlEvent(string thisAccessToken, string thisName)
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
                    name = $"{thisName}",
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
                            GetProperties = getDeviceData.PRproperties;
                        }
                }
            }
        }
        catch (Exception env)
        {

            Console.WriteLine(env.Message);
        }
        return GetProperties;
    }
    public async Task<string> PostAlexaGenerateAToken(string thisCode)
    {
        AlexaAPIDataModel resultData = new AlexaAPIDataModel();

        try
        {
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();

            string payload = JsonConvert.SerializeObject(new
            {
                grant_type = "authorization_code",
                code = $"{thisCode}",
                client_id = "amzn1.application-oa2-client.8e364cf34cb649508a1746e26a4429d4",
                client_secret = "55e478a258cc7e74ad623dd3a5439e501dfad27c8ef710daa7f73b391c98a899",
                redirect_uri = "https://localhost:7016"
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
                            resultData = System.Text.Json.JsonSerializer.Deserialize<AlexaAPIDataModel>(resultContent.Result, options);
                            GetAToken = resultData.AlexaAToken;

                        }
                }
            }
        }
        catch (Exception env)
        {

            Console.WriteLine(env.Message);
        }
        return GetAToken;
    }
}
