using AntDesign.ProLayout;
using BoatRazorLibrary.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<ShellyDevicesData>();
builder.Services.AddSingleton<ShellyDeviceSwitchModeService>();
builder.Services.AddSingleton<ShellyLampModeService>();
builder.Services.AddSingleton<IWeatherForcast, WeatherForecastService>();
builder.Services.AddSingleton<IShellyDevicescs, ShellyDevicesData>();
builder.Services.AddSingleton<IshellyModeService, ShellyDeviceSwitchModeService>();
builder.Services.AddSingleton<IShellyLampModeService, ShellyLampModeService>();
builder.Services.AddSingleton<IBULogin, BULoginService>();
builder.Services.AddSingleton<IBoatsProducts, BoatsProductsServices>();
builder.Services.AddHttpClient("WeatherApi", c =>
{
    c.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?q=miami&appid=8ec38ece4bd75eaf3acb5330aaade5d0");
    c.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
builder.Services.AddAntDesign();
builder.Services.AddControllers()
          .AddJsonOptions(options =>
          {
              options.JsonSerializerOptions.WriteIndented = true;
              options.JsonSerializerOptions.IgnoreNullValues = true;

              options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
              options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
              options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
              options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Default;
          });
builder.Services.AddControllers().AddNewtonsoftJson().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.IgnoreNullValues = true;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Default;
});
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

//builder.Services.AddControllers(options =>
//{
//}).AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
//    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Default;
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
