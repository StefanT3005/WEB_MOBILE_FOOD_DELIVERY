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
            var http = new HttpClient
            {
                // Android emulator: 10.0.2.2 = localhost PC  
                BaseAddress = new Uri("https://localhost:7097/")
            };
            return http;
        });

        builder.Services.AddSingleton<ApiClient>();
        builder.Services.AddSingleton<OrderDraft>();

        // ViewModels
        builder.Services.AddTransient<RestaurantsViewModel>();
        builder.Services.AddTransient<MenuItemsViewModel>();
        builder.Services.AddTransient<PlaceOrderViewModel>();
        builder.Services.AddTransient<OrderStatusViewModel>();

        // Pages
        builder.Services.AddTransient<RestaurantsPage>();
        builder.Services.AddTransient<MenuItemsPage>();
        builder.Services.AddTransient<PlaceOrderPage>();
        builder.Services.AddTransient<OrderStatusPage>();

        builder.Services.AddSingleton<AppShell>();
        return builder.Build();
    }
}
