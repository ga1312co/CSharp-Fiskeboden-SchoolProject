using Informatics.FiskebodenMauiClient.ViewModels;

namespace Informatics.FiskebodenMauiClient.Pages;

public partial class BatchesPage : ContentPage
{
	private readonly BatchesViewModel _viewModel;
	public BatchesPage(BatchesViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_viewModel.RefreshCommand.Execute(null);
    }
}