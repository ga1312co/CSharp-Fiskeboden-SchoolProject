using Informatics.FiskeBoden.Models;
using Informatics.FiskeBoden.ViewModels;
using System;

namespace Informatics.FiskeBoden.Pages;

public partial class AddBatchPage : ContentPage
{
    private readonly BatchesViewModel _viewModel;
    private Batch? _existingBatch;
    private bool _isEditMode;
    private Supplier? _selectedSupplier;
    private Product? _selectedProduct;

    public AddBatchPage(BatchesViewModel viewModel)
    {
        InitializeComponent();
        if (viewModel == null)
        {
            Dispatcher.Dispatch(async () =>
            {
                await DisplayAlert("Critical Error", "ViewModel is null in AddBatchPage constructor!", "OK");
            });

            throw new ArgumentNullException(nameof(viewModel), "ViewModel is null in AddBatchPage constructor!");
        }
        _viewModel = viewModel;
        _isEditMode = false;
        LoadSuppliersAndProducts();
    }

    public AddBatchPage(BatchesViewModel viewModel, Batch existingBatch)
    {
        InitializeComponent();
        if (viewModel == null)
        {
            Dispatcher.Dispatch(async () =>
            {
                await DisplayAlert("Critical Error", "ViewModel is null in AddBatchPage constructor!", "OK");
            });

            throw new ArgumentNullException(nameof(viewModel), "ViewModel is null in AddBatchPage constructor!");
        }
        if (existingBatch == null)
        {
            Dispatcher.Dispatch(async () =>
            {
                await DisplayAlert("Critical Error", "Existing batch cannot be null", "OK");
            });

            throw new ArgumentNullException(nameof(existingBatch), "Existing batch cannot be null");
        }
        _viewModel = viewModel;
        _existingBatch = existingBatch;
        _isEditMode = true;
        Title = "Edit Batch";
        BatchNoEntry.IsEnabled = false;

        // Pre-fill fields with existing batch's attributes
        BatchNoEntry.Text = _existingBatch.BatchNo;
        BatchWeekEntry.Text = _existingBatch.BatchWeek?.ToString();
        BatchQuantityEntry.Text = _existingBatch.BatchQuantity?.ToString();
        
        // New fields
        ProductionDatePicker.Date = _existingBatch.ProductionDate.ToDateTime(TimeOnly.MinValue);
        BatchShelfLifeEntry.Text = _existingBatch.BatchShelfLife.ToString();
        BatchPriceEntry.Text = _existingBatch.BatchPrice.ToString();
        BatchOriginEntry.Text = _existingBatch.BatchOrigin;
        
        _selectedSupplier = _existingBatch.Supplier;
        _selectedProduct = _existingBatch.Product;

        // Update the UI to show selected items in the buttons
        if (_selectedSupplier != null)
        {
            SelectSupplierButton.Text = $"Supplier: {_selectedSupplier.SupplierName}";
        }
        
        if (_selectedProduct != null)
        {
            SelectProductButton.Text = $"Product: {_selectedProduct.ProductName}";
        }

        LoadSuppliersAndProducts();
    }

    private async void LoadSuppliersAndProducts()
    {
        try
        {
            await _viewModel.LoadSuppliersAsync();
            await _viewModel.LoadProductsAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load suppliers or products: {ex.Message}", "OK");
        }
    }

    private async void OnSelectSupplierClicked(object sender, EventArgs e)
    {
        if (_viewModel.Suppliers == null || !_viewModel.Suppliers.Any())
        {
            await DisplayAlert("Error", "No suppliers available.", "OK");
            return;
        }

        // Modified to address warnings by removing the generic parameter constraint
        var selectedSupplier = await ShowVerticalSelectionDialog(
            "Select a supplier", 
            _viewModel.Suppliers.Select(s => s.SupplierName).ToList(),
            s => _viewModel.Suppliers.FirstOrDefault(supplier => supplier.SupplierName == s));

        if (selectedSupplier != null)
        {
            _selectedSupplier = selectedSupplier;
            SelectSupplierButton.Text = $"Supplier: {_selectedSupplier.SupplierName}";
        }
    }

    private async void OnSelectProductClicked(object sender, EventArgs e)
    {
        if (_viewModel.Products == null || !_viewModel.Products.Any())
        {
            await DisplayAlert("Error", "No products available.", "OK");
            return;
        }

        // Modified to address warnings by removing the generic parameter constraint
        var selectedProduct = await ShowVerticalSelectionDialog(
            "Select a product", 
            _viewModel.Products.Select(p => p.ProductName).ToList(),
            p => _viewModel.Products.FirstOrDefault(product => product.ProductName == p));

        if (selectedProduct != null)
        {
            _selectedProduct = selectedProduct;
            SelectProductButton.Text = $"Product: {_selectedProduct.ProductName}";
        }
    }

    // Modified to address nullability warnings
    private async Task<T?> ShowVerticalSelectionDialog<T>(string title, List<string> items, Func<string, T?> selector) 
    {
        // Create a new ContentPage for our custom dialog
        var dialogPage = new ContentPage
        {
            Title = title,
            BackgroundColor = Colors.White
        };

        // Create a TaskCompletionSource to handle the asynchronous result
        var tcs = new TaskCompletionSource<string?>();

        // Create a ScrollView to handle many items
        var scrollView = new ScrollView();
        
        // Create a StackLayout for the items (this ensures vertical layout)
        var stackLayout = new StackLayout
        {
            Padding = new Thickness(20),
            Spacing = 10
        };

        // Add a cancel button at the top
        var cancelButton = new Button
        {
            Text = "Cancel",
            BackgroundColor = Colors.LightCoral,
            CornerRadius = 8,
            Margin = new Thickness(0, 0, 0, 20),
            HeightRequest = 50
        };
        cancelButton.Clicked += (s, e) => 
        {
            tcs.SetResult(null);
            Navigation.PopModalAsync();
        };
        stackLayout.Add(cancelButton);

        // Add all items as buttons
        foreach (var item in items)
        {
            var button = new Button
            {
                Text = item,
                BackgroundColor = Colors.LightBlue,
                CornerRadius = 8,
                HeightRequest = 50,
                Margin = new Thickness(0, 0, 0, 5)
            };
            
            button.Clicked += (s, e) =>
            {
                tcs.SetResult(item);
                Navigation.PopModalAsync();
            };
            
            stackLayout.Add(button);
        }

        scrollView.Content = stackLayout;
        dialogPage.Content = scrollView;

        // Show the dialog modally
        await Navigation.PushModalAsync(dialogPage);

        // Wait for the result
        var result = await tcs.Task;

        // Return the corresponding object or null if canceled
        return result == null ? default : selector(result);
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(BatchNoEntry.Text))
            {
                await DisplayAlert("Error", "Batch No cannot be empty.", "OK");
                return;
            }

            // Check if Batch No already exists
            if (_viewModel.Batches.Any(b => b.BatchNo == BatchNoEntry.Text && (!_isEditMode || (_existingBatch != null && b.BatchId != _existingBatch.BatchId))))
            {
                await DisplayAlert("Error", "Batch No already exists. Please use a different Batch No.", "OK");
                return;
            }

            if (_selectedSupplier == null)
            {
                await DisplayAlert("Error", "Please select a valid supplier.", "OK");
                return;
            }

            if (_selectedProduct == null)
            {
                await DisplayAlert("Error", "Please select a valid product.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(BatchWeekEntry.Text))
            {
                await DisplayAlert("Error", "Batch Week cannot be empty.", "OK");
                return;
            }
            if (!int.TryParse(BatchWeekEntry.Text, out int batchWeek) || batchWeek < 0 || batchWeek > 52)
            {
                await DisplayAlert("Error", "Batch Week must be a valid positive number and not greater than 52.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(BatchQuantityEntry.Text))
            {
                await DisplayAlert("Error", "Batch Quantity cannot be empty.", "OK");
                return;
            }
            if (!decimal.TryParse(BatchQuantityEntry.Text, out decimal batchQuantity) || batchQuantity < 0)
            {
                await DisplayAlert("Error", "Batch Quantity must be a valid positive number.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(BatchShelfLifeEntry.Text))
            {
                await DisplayAlert("Error", "Batch Shelf Life cannot be empty.", "OK");
                return;
            }
            if (!int.TryParse(BatchShelfLifeEntry.Text, out int batchShelfLife) || batchShelfLife < 3)
            {
                await DisplayAlert("Error", "Batch Shelf Life must be at least 3.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(BatchPriceEntry.Text))
            {
                await DisplayAlert("Error", "Batch Price cannot be empty.", "OK");
                return;
            }
            if (!decimal.TryParse(BatchPriceEntry.Text, out decimal batchPrice) || batchPrice < 10)
            {
                await DisplayAlert("Error", "Batch Price must be atleast 10,00.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(BatchOriginEntry.Text))
            {
                await DisplayAlert("Error", "Batch Origin cannot be empty.", "OK");
                return;
            }

            if (_isEditMode)
            {
                _existingBatch!.BatchNo = BatchNoEntry.Text;
                _existingBatch.SupplierId = _selectedSupplier.SupplierId;
                _existingBatch.ProductId = _selectedProduct.ProductId;
                _existingBatch.BatchWeek = int.Parse(BatchWeekEntry.Text);
                _existingBatch.BatchQuantity = decimal.Parse(BatchQuantityEntry.Text);
                _existingBatch.ProductionDate = DateOnly.FromDateTime(ProductionDatePicker.Date);
                _existingBatch.BatchShelfLife = int.Parse(BatchShelfLifeEntry.Text);
                _existingBatch.BatchPrice = decimal.Parse(BatchPriceEntry.Text);
                _existingBatch.BatchOrigin = BatchOriginEntry.Text;

                await _viewModel.UpdateBatchAsync(_existingBatch);
            }
            else
            {
                Batch newBatch = new Batch
                {
                    BatchNo = BatchNoEntry.Text?.Trim() ?? throw new Exception("Batch No is null!"),
                    SupplierId = _selectedSupplier.SupplierId,
                    ProductId = _selectedProduct.ProductId,
                    BatchWeek = int.Parse(BatchWeekEntry.Text),
                    BatchQuantity = decimal.Parse(BatchQuantityEntry.Text),
                    ProductionDate = DateOnly.FromDateTime(ProductionDatePicker.Date),
                    BatchShelfLife = int.Parse(BatchShelfLifeEntry.Text),
                    BatchPrice = decimal.Parse(BatchPriceEntry.Text),
                    BatchOrigin = BatchOriginEntry.Text
                };

                await _viewModel.AddBatchAsync(newBatch);
            }

            Dispatcher.Dispatch(async () =>
            {
                await DisplayAlert("Success", "Batch saved successfully!", "OK");
                await Navigation.PopModalAsync();
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Something went wrong: {ex.Message}", "OK");
        }
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
