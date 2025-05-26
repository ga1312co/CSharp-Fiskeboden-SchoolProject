using Informatics.FiskeBoden.Models;
using Informatics.FiskeBoden.Data;
using Microsoft.EntityFrameworkCore;
using Informatics.FiskeBoden.Interfaces;

namespace Informatics.FiskeBoden.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.Include(c => c.Products).ToListAsync();
    }

    public async Task<Category?> GetCategoryAsync(int categoryId)
    {
        return await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        Category? category = await _context.Categories.FindAsync(categoryId);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
