using Informatics.FiskebodenWebAPI.Contexts;
using Informatics.FiskebodenWebAPI.DTOs;
using Informatics.FiskebodenWebAPI.Extensions;
using Informatics.FiskebodenWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Informatics.FiskebodenWebAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly FiskeBodenContext _context;

    public CategoryService(FiskeBodenContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories =  await _context.Categories
            .Include(c => c.Products)
            .ToListAsync();

        return categories.Select(c => c.ToDto());
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.CategoryId == id);

        return category?.ToDto();
    }
}
