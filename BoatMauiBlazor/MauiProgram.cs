using BoatRazorLibrary.Models;
using System.Text.Encodings.Web;

namespace BoatMauiBlazor;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		builder.Services.AddHttpClient("WeatherApi", c =>
		{
			c.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?q=miami&appid=8ec38ece4bd75eaf3acb5330aaade5d0");
			c.DefaultRequestHeaders.Add("Accept", "application/json");
		});
		builder.Services.AddControllers().AddNewtonsoftJson();
		builder.Services.AddControllers(options =>
		{
		}).AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
			options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Default;
		});

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
		builder.Services.AddSingleton<WeatherForecastService>();
		builder.Services.AddSingleton<IWeatherForcast, WeatherForecastService>();
		builder.Services.AddSingleton<IBULogin, BULoginService>();
		builder.Services.AddSingleton<IBoatsProducts, BoatsProductsServices>();

		return builder.Build();
	}
}
