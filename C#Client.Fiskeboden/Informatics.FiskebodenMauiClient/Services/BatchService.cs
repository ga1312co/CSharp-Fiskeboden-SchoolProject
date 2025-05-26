using System.Diagnostics;
using System.Text.Json;
using Informatics.FiskebodenMauiClient.Models;

namespace Informatics.FiskebodenMauiClient.Services;

public class BatchService : IBatchService
{

    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = "http://localhost:5211/api/";

    public BatchService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_apiUrl);
    }

    public async Task<IEnumerable<Batch>> GetBatchesAsync()
    {
        try
        {
            var responseStream = await _httpClient.GetStreamAsync("batches");
            var batches = await JsonSerializer.DeserializeAsync<List<Batch>>(responseStream) ?? new List<Batch>();
            return batches;
        }
        catch (HttpRequestException ex)
        {
            Debug.WriteLine($"HTTP request error: {ex.Message}");
            return new List<Batch>();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"General error: {ex.Message}");
            return new List<Batch>();
        }
    }
}
