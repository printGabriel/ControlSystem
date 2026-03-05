using ControlSystem.Application.DTOs;
using System.Threading.Tasks;

namespace ControlSystem.Application.Interfaces
{
    public interface ICategoryAppService
    {
        Task<CategoryDto> CreateCategory(CategoryDto command);
        CategoryDto? GetCategoryById(int categoryId);
        List<CategoryDto> GetAllCategories();
        Task<CategoryDto?> UpdateCategory(CategoryDto command);
        bool DeleteCategoryById(int categoryId);
    }
}