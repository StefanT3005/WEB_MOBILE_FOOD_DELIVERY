using Microsoft.Extensions.Logging;
using MOBILE.Services;
using MOBILE.ViewModels;
using MOBILE.Views;

namespace MOBILE;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // HttpClient
        builder.Services.AddSingleton(sp =>
        {
            var http = new HttpClient();
            http.BaseAddress = new Uri("https://10.0.2.2:7097/"); // emulator Android
            return http;
        });

        builder.Services.AddSingleton<ApiClient>();

        builder.Services.AddTransient<PlaceOrderViewModel>();
        builder.Services.AddTransient<OrderStatusViewModel>();

        builder.Services.AddTransient<PlaceOrderPage>();
        builder.Services.AddTransient<OrderStatusPage>();

        return builder.Build();
    }
}
