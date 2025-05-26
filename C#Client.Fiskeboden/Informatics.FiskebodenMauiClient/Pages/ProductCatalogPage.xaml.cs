namespace Informatics.FiskebodenMauiClient.Pages;
using Informatics.FiskebodenMauiClient.ViewModels;

public partial class ProductCatalogPage : ContentPage
{
    private readonly ProductsViewModel _viewModel;

    public ProductCatalogPage(ProductsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        _viewModel.NoProductsFound += OnNoProductsFound;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.RefreshCommand.Execute(null);
    }
    private async void OnNoProductsFound(string searchText)
    {
        await DisplayAlert("No Products Found", $"No product found with {searchText}", "OK");
    }
}