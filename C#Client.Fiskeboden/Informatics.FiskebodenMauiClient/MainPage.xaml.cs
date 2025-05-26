namespace Informatics.FiskebodenMauiClient;
using Informatics.FiskebodenMauiClient.Services;
using Informatics.FiskebodenMauiClient.ViewModels;
using Informatics.FiskebodenMauiClient.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnBrowseCatalogClicked(object sender, EventArgs e)
    {
        var productService = new ProductService();
        await Shell.Current.GoToAsync(nameof (ProductCatalogPage));
    }

    private async void OnBrowseBatchesClicked(object sender, EventArgs e)
    {
        var batchService = new BatchService();
        await Shell.Current.GoToAsync(nameof (BatchesPage));
    }
}