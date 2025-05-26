using System.Collections.Generic;
using System.Threading.Tasks;
using Informatics.FiskeBoden.Models;

namespace Informatics.FiskeBoden.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product?> GetProductAsync(int productId);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task AddProductAsync(Product product);

        Task<bool> CategoryExistsAsync(int categoryId);
    }
}