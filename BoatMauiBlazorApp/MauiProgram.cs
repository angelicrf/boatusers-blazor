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
                })
                .ConfigureEssentials(essential => essential.UseMapServiceToken("s45dMhh0pAiusDVwJ0sY~b2kicPVNhC0OujmLLYpsVA~Aofz5AaGRO5khLPq3FRxL9FQNlo6zXHPJTxB_puCIKMztKCAtj2Bq3Zcr7dn5g0R"));


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
            builder.Services.AddSingleton<ShellyDevicesData>();
            builder.Services.AddSingleton<ShellyDeviceSwitchModeService>();
            builder.Services.AddSingleton<ShellyLampModeService>();
            builder.Services.AddSingleton<IWeatherForcast, WeatherForecastService>();
            builder.Services.AddSingleton<IShellyDevicescs, ShellyDevicesData>();
            builder.Services.AddSingleton<IshellyModeService, ShellyDeviceSwitchModeService>();
            builder.Services.AddSingleton<IShellyLampModeService, ShellyLampModeService>();
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