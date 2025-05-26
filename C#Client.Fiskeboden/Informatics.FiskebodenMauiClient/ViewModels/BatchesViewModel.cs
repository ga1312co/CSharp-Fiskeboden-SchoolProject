using System;
using Informatics.FiskebodenMauiClient.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Informatics.FiskebodenMauiClient.Services;

namespace Informatics.FiskebodenMauiClient.ViewModels;

public class BatchesViewModel
{
    private readonly IBatchService _batchService;
    public ObservableCollection<Batch> Batches { get; set; }
    public ICommand RefreshCommand { get; set; }

    public BatchesViewModel(IBatchService batchService)
    {
        _batchService = batchService;
        Batches = new ObservableCollection<Batch>();
        RefreshCommand = new Command(async () => await LoadBatchesAsync());
    }

    private async Task LoadBatchesAsync()
    {
        var batches = await _batchService.GetBatchesAsync();
        Batches.Clear();
        foreach (var batch in batches)
        {
            Batches.Add(batch);
        }
    }


}
