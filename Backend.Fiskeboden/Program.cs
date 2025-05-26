using Informatics.FiskebodenWebAPI.Contexts;
using Microsoft.EntityFrameworkCore;
using Informatics.FiskebodenWebAPI.Services;
using Informatics.FiskebodenWebAPI.Services.Interfaces;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

/*
* För att testa API calls i scalar så 
* kör projektet och skriv sedan in localhost:5211/scalar/v1 i webbläsaren
*/

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<FiskeBodenContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBatchService, BatchService>();
builder.Services.AddScoped<ISupplierPickupPointService, SupplierPickupPointService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPickupPointService, PickupPointService>();

// Registers services needed for using controllers
builder.Services.AddControllers();


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Ensures that all HTTP requests are redirected to HTTPS (more secure)
app.UseHttpsRedirection();

// Enables endpoint routing - match incoming requests to appropriate route handlers
app.UseRouting();

// Maps controller endpoints so the app can handle requests using defined controller actions
app.MapControllers();

app.Run();
