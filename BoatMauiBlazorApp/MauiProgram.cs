using AntDesign.ProLayout;
using BoatRazorLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace BoatMauiBlazorApp
{

    public static class MauiProgram
    {
        public static IConfiguration configuration;
        public static Action<ProSettings> antSetting;
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
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Services.AddOptions<ProSettings>()
        .Configure<IConfiguration>((options, configuration) =>
         configuration.GetSection("ProSettings").Bind(options));
            builder.Services.AddAntDesign();
#endif

            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton<IWeatherForcast, WeatherForecastService>();
            builder.Services.AddSingleton<IBULogin, BULoginService>();
            builder.Services.AddSingleton<IBoatsProducts, BoatsProductsServices>();
            builder.Services.AddOptions<ProSettings>()
           .Configure<IConfiguration>((options, configuration) =>
            configuration.GetSection("ProSettings").Bind(options));
            builder.Services.AddAntDesign();
            return builder.Build();
        }
    }
}