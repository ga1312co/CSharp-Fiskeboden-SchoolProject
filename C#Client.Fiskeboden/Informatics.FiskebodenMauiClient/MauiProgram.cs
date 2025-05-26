using Microsoft.Extensions.Logging;
using Informatics.FiskebodenMauiClient.Services;
using Informatics.FiskebodenMauiClient.ViewModels;
using Informatics.FiskebodenMauiClient.Pages;

namespace Informatics.FiskebodenMauiClient;

public static class MauiProgram
{
    public static IServiceProvider Services { get; private set; }

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

        // Register services
        builder.Services.AddSingleton<IProductService, ProductService>();
        builder.Services.AddSingleton<IBatchService, BatchService>();
        // Register viewmodels
        builder.Services.AddSingleton<BatchesViewModel>();
        builder.Services.AddTransient<ProductsViewModel>();
        // Register pages
        builder.Services.AddTransient<BatchesPage>();
        builder.Services.AddTransient<ProductCatalogPage>();
        builder.Services.AddTransient<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        var app = builder.Build();
        Services = app.Services;
        return app;
    }
}