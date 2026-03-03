using ControlSystem.Application.DTOs;
using System.Threading.Tasks;

namespace ControlSystem.Application.Interfaces
{
    public interface ICategoryAppService
    {
        CategoryDto CreateCategory(CategoryDto command);
        CategoryDto? GetCategoryById(int categoryId);
        Task<CategoryDto?> UpdateCategory(CategoryDto command);
        bool DeleteCategoryById(int categoryId);
    }
}