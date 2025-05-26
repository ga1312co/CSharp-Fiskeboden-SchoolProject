using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Reflection;
using Informatics.FiskeBoden.Data;
using Informatics.FiskeBoden.Interfaces;
using Informatics.FiskeBoden.Services;
using Informatics.FiskeBoden.Pages;
using Informatics.FiskeBoden.ViewModels;

namespace Informatics.FiskeBoden;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Read `appsettings.json`
        System.Reflection.Assembly assembly = Assembly.GetExecutingAssembly();
        string appsettingsFileName = "Informatics.FiskeBoden.appsettings.json";
        using (var stream = assembly.GetManifestResourceStream(appsettingsFileName))
        {
            if (stream != null)
            {
                builder.Configuration.AddJsonStream(stream);
            }
        }

        // Collect connectionString
        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Connection string not found in appsettings.json.");
        }

        // Add DB-context
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register Services
        // Singleton for services that should be instantiated once
        builder.Services.AddSingleton<ISupplierService, SupplierService>();
        builder.Services.AddSingleton<ISupplierPickupPointService, SupplierPickupPointService>();
        builder.Services.AddSingleton<IBatchService, BatchService>();
        builder.Services.AddSingleton<IProductService, ProductService>();
        builder.Services.AddSingleton<ICategoryService, CategoryService>();
        builder.Services.AddSingleton<IPickupPointService, PickupPointService>();
        builder.Services.AddSingleton<IChatGptService, ChatGptService>();

        // Transient for ViewModels
        builder.Services.AddTransient<SupplierViewModel>();
        builder.Services.AddTransient<BatchesViewModel>();
        builder.Services.AddTransient<ProductsViewModel>();
        builder.Services.AddTransient<AdPageViewModel>();

        // Transient for Pages
        builder.Services.AddTransient<BatchesPage>();
        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<SupplierPage>();
        builder.Services.AddTransient<AddBatchPage>();
        builder.Services.AddTransient<AddProductPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

}

