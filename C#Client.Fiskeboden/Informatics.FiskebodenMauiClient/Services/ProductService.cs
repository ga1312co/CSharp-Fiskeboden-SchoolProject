using System.Text.Json;
using Informatics.FiskebodenMauiClient.Models;

namespace Informatics.FiskebodenMauiClient.Services;
public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = "http://localhost:5211/api/";

    public ProductService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_apiUrl);

    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        try
        {
            var responseStream = await _httpClient.GetStreamAsync("products");
            var products = await JsonSerializer.DeserializeAsync<List<Product>>(responseStream) ?? new List<Product>();
            return products;
        }
        catch (HttpRequestException)
        {
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Product> GetProductAsync(string id)
    {
        try
        {
            var responseStream = await _httpClient.GetStreamAsync($"products/{id}");
            var product = await JsonSerializer.DeserializeAsync<Product>(responseStream);
            return product;
        }
        catch (HttpRequestException)
        {
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}