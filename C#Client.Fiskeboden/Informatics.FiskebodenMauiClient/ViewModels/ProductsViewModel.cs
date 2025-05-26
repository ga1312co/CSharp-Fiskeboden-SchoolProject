using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Informatics.FiskebodenMauiClient.Models;
using Informatics.FiskebodenMauiClient.Services;

namespace Informatics.FiskebodenMauiClient.ViewModels;
public class ProductsViewModel
{
    public event Action<string> NoProductsFound;
    private readonly IProductService _productService;
    public ObservableCollection<Product> Products { get; set; }
    public string SearchText { get; set; }
    public ICommand RefreshCommand { get; set; }
    public ICommand SearchCommand { get; set; }

    public ProductsViewModel(IProductService productService)
    {
        _productService = productService;
        Products = new ObservableCollection<Product>();
        RefreshCommand = new Command(async () => await LoadProductsAsync());
        SearchCommand = new Command(async () => await SearchProductAsync());
    }

    private async Task LoadProductsAsync()
    {
        var products = await _productService.GetProductsAsync();
        Products.Clear();
        foreach (var product in products)
        {
            Products.Add(product);
        }
    }

    private async Task SearchProductAsync()
    {
        Console.WriteLine($"SearchProductAsync called with SearchText: {SearchText}");

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            await LoadProductsAsync(); // Load all products if search text is empty
            return;
        }

        // Get all products and filter locally
        var allProducts = await _productService.GetProductsAsync();
        var filteredProducts = allProducts?.Where(p =>
            p.ProductNo.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();

        Products.Clear();
        if (filteredProducts != null && filteredProducts.Any())
        {
            foreach (var product in filteredProducts)
            {
                Products.Add(product);
            }
        }
        else
        {
            NoProductsFound?.Invoke(SearchText);
            Console.WriteLine("No products found matching the criteria");
        }
    }

}