using BoatRazorLibrary.Models;

namespace BoatMauiBlazorApp
{
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
}