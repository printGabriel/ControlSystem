using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Interfaces;
using ControlSystem.Infra.Data.Context;
using System.Linq;
using System.Threading.Tasks;

namespace ControlSystem.Infra.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _context;

        public CategoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Category> Add(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public Category Get(int id)
        {
            var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            return category;
        }

        public List<Category> GetAll()
        {
            var category = _context.Categories.ToList();
            return category;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public bool Delete(int id)
        {
            var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();

            if (category != null)
            {
                _context.Remove(category);
                _context.SaveChanges();
            }

            return true;
        }
    }
}