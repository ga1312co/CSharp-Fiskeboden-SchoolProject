using System;
using Microsoft.Maui.Controls;
using Informatics.FiskeBoden.ViewModels;
using Informatics.FiskeBoden.Interfaces;
using Informatics.FiskeBoden.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Informatics.FiskeBoden.Pages
{
    public partial class AdPage : ContentPage
    {
		private readonly AdPageViewModel _viewModel;
        public AdPage()
        {
            InitializeComponent();
            
            var app = Application.Current as App;
            if (app?.Services != null)
            {
                var batchService = app.Services.GetRequiredService<IBatchService>();
                var chatGptService = app.Services.GetRequiredService<IChatGptService>();
                
                _viewModel = new AdPageViewModel(batchService, chatGptService);
                BindingContext = _viewModel;
            }
            else
            {
                throw new InvalidOperationException("Required services are not available");
            }
        }
    

		private async void OnGenerateAdClicked(object sender, EventArgs e)
		{
			if (int.TryParse(BatchWeekEntry.Text, out int inputWeek))
			{
				await _viewModel.GenerateAdForWeekAsync(inputWeek);
			}
			else
			{
				await DisplayAlert("Error", "Please enter a valid numeric week.", "OK");
			}
		}
	}
}