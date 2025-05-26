using System.Collections.Generic;
using System.Threading.Tasks;
using Informatics.FiskeBoden.Interfaces;
using Informatics.FiskeBoden.Models;
using Informatics.FiskeBoden.Data;
using Microsoft.EntityFrameworkCore;

namespace Informatics.FiskeBoden.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products
                                    .Include(p => p.Category)
                                    .ToListAsync();
        }

        public async Task<Product?> GetProductAsync(int productId)
        {
            return await _context.Products
                                    .Include(p => p.Category)
                                    .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(int productId)
        {
            Product? product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryId == categoryId);
        }
    }
}
