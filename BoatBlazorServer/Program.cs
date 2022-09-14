using AntDesign.ProLayout;
using BoatRazorLibrary.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<IWeatherForcast, WeatherForecastService>();
builder.Services.AddSingleton<IBULogin, BULoginService>();
builder.Services.AddSingleton<IBoatsProducts, BoatsProductsServices>();
builder.Services.AddHttpClient("WeatherApi", c =>
{
    c.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?q=miami&appid=8ec38ece4bd75eaf3acb5330aaade5d0");
    c.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
builder.Services.AddAntDesign();
//builder.Services.AddControllers().AddNewtonsoftJson();
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
