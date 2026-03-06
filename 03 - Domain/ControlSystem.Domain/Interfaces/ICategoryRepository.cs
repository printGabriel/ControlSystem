using ControlSystem.Domain.Entities;
using System.Threading.Tasks;

namespace ControlSystem.Domain.Interfaces
{
    // Interface responsável pelas operações de acesso aos dados de categorias
    // aqui ficam apenas as assinaturas dos métodos que manipulam elas no banco
    public interface ICategoryRepository
    {
        // Adiciona uma nova categoria
        Task<Category> Add(Category category);

        // Busca uma categoria pelo Id
        Category Get(int id);

        // Retorna todas as categorias cadastradas
        List<Category> GetAll();

        // Salva alterações no banco
        Task Save();

        // Remove uma categoria pelo Id
        bool Delete(int id);

        // Verifica se já existe uma categoria com a mesma descrição
        bool DuplicateCategory(int id, string description);
    }
}