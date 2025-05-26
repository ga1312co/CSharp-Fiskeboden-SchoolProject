using Informatics.FiskeBoden.ViewModels;
using Informatics.FiskeBoden.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Informatics.FiskeBoden.Models;

namespace Informatics.FiskeBoden.Pages;

public partial class ProductsPage : ContentPage
{
    private readonly ProductsViewModel? _viewModel;
    private Product? _selectedProduct;

    public ProductsPage(IProductService productService, ICategoryService categoryService)
    {
        InitializeComponent();

        _viewModel = new ProductsViewModel(
            productService ?? throw new ArgumentNullException(nameof(productService)),
            categoryService ?? throw new ArgumentNullException(nameof(categoryService))
        );

        BindingContext = _viewModel;
    }

    public ProductsPage()
    {
        InitializeComponent();

        var app = Application.Current as App;
        if (app?.Services == null)
        {
            throw new Exception("Application services are missing.");
        }

        var productService = app.Services.GetService<IProductService>()
            ?? throw new Exception("ProductService is missing from DI.");

        var categoryService = app.Services.GetService<ICategoryService>()
            ?? throw new Exception("CategoryService is missing from DI.");

        _viewModel = new ProductsViewModel(productService, categoryService);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel != null)
        {
            await _viewModel.LoadProductsAsync();
            await _viewModel.LoadCategoriesAsync();
            UpdateTableView();
        }
    }

    private async void OnAddProductClicked(object sender, EventArgs e)
    {
        if (_viewModel != null)
        {
            await Navigation.PushModalAsync(new AddProductPage(_viewModel));
        }
        else
        {
            await DisplayAlert("Error", "ViewModel is not available.", "OK");
        }
    }

    private async void OnRemoveProductClicked(object sender, EventArgs e)
    {
        if (_viewModel != null && _selectedProduct != null)
        {
            bool confirm = await DisplayAlert("Confirm Deletion",
                $"Are you sure you want to remove {_selectedProduct.ProductName}?", "Yes", "No");
            if (confirm)
            {
                await _viewModel.RemoveProductAsync(_selectedProduct);
                _selectedProduct = null; // Clear selection after removal
                UpdateTableView();
            }
        }
        else
        {
            await DisplayAlert("Error", "Please select a product to remove.", "OK");
        }
    }

    private async void OnEditProductClicked(object sender, EventArgs e)
    {
        if (_viewModel == null)
        {
            await DisplayAlert("Error", "ViewModel is not available.", "OK");
            return;
        }

        if (_selectedProduct == null)
        {
            await DisplayAlert("Error", "Please select a product to edit.", "OK");
            return;
        }

        await Navigation.PushModalAsync(new AddProductPage(_viewModel, _selectedProduct));
    }

    private void UpdateTableView()
    {
        if (ProductsTableView == null || _viewModel == null)
        {
            return;
        }

        var root = new TableRoot();
        var section = new TableSection("Products");

        // Add header row
        var headerGrid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            },
            BackgroundColor = Colors.LightGoldenrodYellow,
            Padding = new Thickness(5),
        };

        var headerLabels = new[]
        {
            new Label { Text = "Product No", FontAttributes = FontAttributes.Bold, TextColor = Colors.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center },
            new Label { Text = "Product Name", FontAttributes = FontAttributes.Bold, TextColor = Colors.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center },
            new Label { Text = "Measurement Type", FontAttributes = FontAttributes.Bold, TextColor = Colors.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center },
            new Label { Text = "Category Name", FontAttributes = FontAttributes.Bold, TextColor = Colors.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center },
        };

        for (int i = 0; i < headerLabels.Length; i++)
        {
            headerGrid.Children.Add(headerLabels[i]);
            Grid.SetColumn(headerLabels[i], i);
        }

        section.Add(new ViewCell { View = headerGrid });

        foreach (var product in _viewModel.Products)
        {
            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            var productNoLabel = new Label { Text = product.ProductNo, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
            var productNameLabel = new Label { Text = product.ProductName, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
            var isMeasuredInUnitsLabel = new Label { Text = product.IsMeasuredInUnits ? "St" : "Kg", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
            var categoryLabel = new Label { Text = _viewModel.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId)?.CategoryName ?? "Unknown", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };

            grid.Children.Add(productNoLabel);
            Grid.SetColumn(productNoLabel, 0);

            grid.Children.Add(productNameLabel);
            Grid.SetColumn(productNameLabel, 1);

            grid.Children.Add(isMeasuredInUnitsLabel);
            Grid.SetColumn(isMeasuredInUnitsLabel, 2);

            grid.Children.Add(categoryLabel);
            Grid.SetColumn(categoryLabel, 3);

            var viewCell = new ViewCell { View = grid };
            viewCell.Tapped += (s, e) =>
            {
                _selectedProduct = product;
                UpdateTableView(); // Refresh to show selection
            };

            // Highlight the selected product
            if (_selectedProduct != null && _selectedProduct.ProductId == product.ProductId)
            {
                viewCell.View.BackgroundColor = Colors.LightGray;
            }

            section.Add(viewCell);
        }

        root.Add(section);
        ProductsTableView.Root = root;
    }
}
