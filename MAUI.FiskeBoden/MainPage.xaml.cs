using Microsoft.Maui.Controls;
using System;
using Informatics.FiskeBoden.Pages;
using Microsoft.Extensions.DependencyInjection;
using Informatics.FiskeBoden.Interfaces;

namespace Informatics.FiskeBoden
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnBatchesClicked(object sender, EventArgs e)
        {
            var app = Application.Current as App;
            if (app?.Services != null)
            {
                var batchService = app.Services.GetRequiredService<IBatchService>();
                var productService = app.Services.GetRequiredService<IProductService>();
                var supplierService = app.Services.GetRequiredService<ISupplierService>();

                await Navigation.PushAsync(new BatchesPage(batchService, productService, supplierService));
            }
            else
            {
                await DisplayAlert("Error", "Unable to load batches. Please try again later.", "OK");
            }
        }

        private async void OnProductsClicked(object sender, EventArgs e)
        {
            var app = Application.Current as App;
            if (app?.Services != null)
            {
                var productService = app.Services.GetRequiredService<IProductService>();
                var categoryService = app.Services.GetRequiredService<ICategoryService>(); 

                await Navigation.PushAsync(new ProductsPage(productService, categoryService)); 
            }
            else
            {
                await DisplayAlert("Error", "Unable to load products. Please try again later.", "OK");
            }
        }

        private async void OnSupplierTapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new Informatics.FiskeBoden.Pages.SupplierPage());
        }

        private async void OnAdMakerClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Informatics.FiskeBoden.Pages.AdPage());
        }
    }
}
