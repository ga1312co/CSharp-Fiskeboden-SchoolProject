using Informatics.FiskebodenWebAPI.DTOs;

namespace Informatics.FiskebodenWebAPI.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int id);

}
