using ControlSystem.Application.DTOs;
using System.Threading.Tasks;

namespace ControlSystem.Application.Interfaces
{
    // Interface responsável pelas operações relacionadas as categorias na camada de aplicação.
    public interface ICategoryAppService
    {
        // Cria uma nova categoria
        Task<CategoryDto> CreateCategory(CategoryDto command);

        // Busca uma categoria pelo Id
        CategoryDto? GetCategoryById(int categoryId);

        // Retorna todas as categorias cadastradas
        List<CategoryDto> GetAllCategories();

        // Atualiza uma categoria existente
        Task<CategoryDto?> UpdateCategory(CategoryDto command);

        // Remove uma categoria pelo Id
        bool DeleteCategoryById(int categoryId);
    }
}