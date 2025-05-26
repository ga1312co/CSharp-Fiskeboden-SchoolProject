using Informatics.FiskebodenWebAPI.DTOs;

namespace Informatics.FiskebodenWebAPI.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();

    Task<ProductDto?> GetProductByIdAsync(int id);

}
