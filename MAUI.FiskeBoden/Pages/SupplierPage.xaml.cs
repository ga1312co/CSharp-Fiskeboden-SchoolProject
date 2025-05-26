using Informatics.FiskeBoden.ViewModels;
using Informatics.FiskeBoden.Interfaces;
using Informatics.FiskeBoden.Services;

namespace Informatics.FiskeBoden.Pages;

public partial class SupplierPage : ContentPage
{
    private readonly SupplierViewModel _viewModel;

    public SupplierPage()
    {
        InitializeComponent();
        ISupplierService? supplierService = ((App?)Application.Current)?.Services.GetRequiredService<ISupplierService>();
        IBatchService? batchService = ((App?)Application.Current)?.Services.GetRequiredService<IBatchService>();
        if (supplierService == null || batchService == null)
        {
            throw new InvalidOperationException("Required services are not available.");
        }
        _viewModel = new SupplierViewModel(supplierService, batchService);
        BindingContext = _viewModel;
    }

    public SupplierPage(SupplierViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadSuppliersAsync();
    }
}