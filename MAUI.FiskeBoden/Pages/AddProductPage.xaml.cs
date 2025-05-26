using Informatics.FiskeBoden.Models;
using Informatics.FiskeBoden.ViewModels;
using Informatics.FiskeBoden.Interfaces;
using System.Linq;

namespace Informatics.FiskeBoden.Pages;

public partial class AddProductPage : ContentPage
{
    private readonly ProductsViewModel _viewModel;
    private Product? _existingProduct;
    private bool _isEditMode;
    private Category? _selectedCategory;

    public AddProductPage(ProductsViewModel viewModel)
    {
        InitializeComponent();

        if (viewModel == null)
        {
            Dispatcher.Dispatch(async () =>
            {
                await DisplayAlert("Critical Error", "ViewModel is null in AddProductPage constructor!", "OK");
            });

            throw new ArgumentNullException(nameof(viewModel), "ViewModel is null in AddProductPage constructor!");
        }

        _viewModel = viewModel;
        _isEditMode = false;
        LoadCategories();
    }

    public AddProductPage(ProductsViewModel viewModel, Product existingProduct)
    {
        InitializeComponent();

        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel), "ViewModel is null in AddProductPage constructor!");
        _existingProduct = existingProduct ?? throw new ArgumentNullException(nameof(existingProduct), "Existing product cannot be null");

        _isEditMode = true;
        Title = "Edit Product";
        ProductNoEntry.IsEnabled = false;

        // Pre-fill fields with existing product's attributes
        ProductNoEntry.Text = _existingProduct.ProductNo;
        ProductNameEntry.Text = _existingProduct.ProductName;
        StRadioButton.IsChecked = _existingProduct.IsMeasuredInUnits;
        _selectedCategory = _viewModel.Categories.FirstOrDefault(c => c.CategoryId == _existingProduct.CategoryId);
        CategoryPicker.SelectedItem = _selectedCategory;

        LoadCategories();
    }

    private async void LoadCategories()
    {
        try
        {
            CategoryPicker.ItemsSource = null; // Förhindrar crash om Categories är null

            await _viewModel.LoadCategoriesAsync();

            if (_viewModel.Categories == null || !_viewModel.Categories.Any())
            {
                await DisplayAlert("Error", "No categories found in database.", "OK");
                return;
            }

            CategoryPicker.ItemsSource = _viewModel.Categories;

            if (_isEditMode && _existingProduct != null)
            {
                PopulateFields();
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load categories: {ex.Message}", "OK");
        }
    }

    private void PopulateFields()
    {
        if (_existingProduct == null) return;

        ProductNoEntry.Text = _existingProduct.ProductNo;
        ProductNoEntry.IsEnabled = false;
        ProductNameEntry.Text = _existingProduct.ProductName;
        StRadioButton.IsChecked = _existingProduct.IsMeasuredInUnits;
        KgRadioButton.IsChecked = !_existingProduct.IsMeasuredInUnits;

        _selectedCategory = _viewModel.Categories?.FirstOrDefault(c => c.CategoryId == _existingProduct.CategoryId);
        CategoryPicker.SelectedItem = _selectedCategory;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        try
        {

            if (!await ValidateInput())
            {
                return;
            }

            if (_selectedCategory == null)
            {
                await DisplayAlert("Error", "Please select a category.", "OK");
                return;
            }
            
            if (!StRadioButton.IsChecked && !KgRadioButton.IsChecked)
            {
                await DisplayAlert("Error", "Please select a measurement unit.", "OK");
                return;
            }

            if (_isEditMode)
            {
                _existingProduct!.ProductName = ProductNameEntry.Text;
                _existingProduct.IsMeasuredInUnits = StRadioButton.IsChecked;
                _existingProduct.CategoryId = _selectedCategory.CategoryId;

                await _viewModel.UpdateProductAsync(_existingProduct);
            }
            else
            {
                string productNo = ProductNoEntry.Text?.Trim() ?? throw new Exception("Product No is null!");
                string productName = ProductNameEntry.Text?.Trim() ?? throw new Exception("Product Name is null!");

                Product newProduct = new Product
                {
                    ProductNo = productNo,
                    ProductName = productName,
                    IsMeasuredInUnits = StRadioButton.IsChecked
                };

                if (_selectedCategory != null)
                {
                    newProduct.CategoryId = _selectedCategory.CategoryId;
                }
                else
                {
                    await DisplayAlert("Error","No category selected, setting default CategoryId.", "OK");
                }

                await _viewModel.AddProductAsync(newProduct);
            }

            Dispatcher.Dispatch(async () =>
            {
                await DisplayAlert("Success", "Product added successfully!", "OK");
                await Navigation.PopModalAsync();
            });


        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Something went wrong: {ex.Message}", "OK");
        }
    }

    private async Task<bool> ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(ProductNoEntry.Text) ||
            string.IsNullOrWhiteSpace(ProductNameEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all required fields.", "OK");
            return false;
        }

        if (!_isEditMode && _viewModel.Products.Any(p => p.ProductNo == ProductNoEntry.Text))
        {
            await DisplayAlert("Error", "Product No already exists.", "OK");
            return false;
        }

        return true;
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        _selectedCategory = CategoryPicker.SelectedItem as Category;
    }
}
