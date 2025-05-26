using System.Collections.ObjectModel;
using Informatics.FiskeBoden.Interfaces;
using Informatics.FiskeBoden.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Informatics.FiskeBoden.ViewModels
{
    public class ProductsViewModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

        public ProductsViewModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task LoadProductsAsync()
        {
            var products = await _productService.GetProductsAsync();
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        public async Task AddProductAsync(Product product)
        {
            await _productService.CreateProductAsync(product);
            Products.Add(product);
        }

        public async Task RemoveProductAsync(Product product)
        {
            await _productService.DeleteProductAsync(product.ProductId);
            Products.Remove(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productService.UpdateProductAsync(product);
        }

        public async Task LoadCategoriesAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            Categories.Clear();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _categoryService.GetCategoryAsync(categoryId) != null;
        }
    }
}
