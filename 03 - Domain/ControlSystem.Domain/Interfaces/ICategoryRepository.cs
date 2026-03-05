using ControlSystem.Domain.Entities;
using System.Threading.Tasks;

namespace ControlSystem.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> Add(Category category);
        Category Get(int id);
        List<Category> GetAll();
        Task Save();
        bool Delete(int id);
        bool DuplicateCategory(int id, string description);
    }
}