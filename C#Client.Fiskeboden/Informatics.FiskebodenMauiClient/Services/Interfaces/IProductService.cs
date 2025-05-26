using System.Collections.Generic;
using System.Threading.Tasks;
using Informatics.FiskebodenMauiClient.Models;

namespace Informatics.FiskebodenMauiClient.Services;
public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync();

    Task<Product> GetProductAsync(string id);
}