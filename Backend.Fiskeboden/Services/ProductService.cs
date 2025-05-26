using Informatics.FiskebodenWebAPI.Contexts;
using Informatics.FiskebodenWebAPI.DTOs;
using Informatics.FiskebodenWebAPI.Extensions;
using Informatics.FiskebodenWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Informatics.FiskebodenWebAPI.Services;

public class ProductService : IProductService
{
    private readonly FiskeBodenContext _context;

    public ProductService(FiskeBodenContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Batches)
            .ToListAsync();

        return products.Select(p => p.ToDto());
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Batches)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        return product?.ToDto();
    }
}
