using Informatics.FiskeBoden.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Informatics.FiskeBoden.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryAsync(int categoryId);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId);
    }
}
