using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Enums;
using ControlSystem.Domain.Interfaces;
using ControlSystem.Infra.Data.Context;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;

namespace ControlSystem.Infra.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        // Repositório responsável por acessar os dados das categorias no banco, usando entity framework
        private readonly ApplicationContext _context;

        // Injeção de dependência do contexto do banco para acessar os dados
        public CategoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        //adiciona um novo registro de categoria no banco
        public async Task<Category> Add(Category category)
        {
            // Verifica se já existe categoria com mesma descrição
            DuplicateCategory(category.Id, category.Description);

            //caso não exista, adiciona e salva no banco
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        //busca por uma categoria específica por seu id
        public Category Get(int id)
        {
            var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            return category;
        }

        //busca por todas as categorias no banco
        public List<Category> GetAll()
        {
            var category = _context.Categories.ToList();
            return category;
        }

        //save utilizado em updates de categorias
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        //deleta categoria específica pelo id
        public bool Delete(int id)
        {
            var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();

            if (category != null)
            {
                _context.Remove(category);
                _context.SaveChanges();
            }
            else
                return false;

            return true;
        }

        //verifica se existe outra categoria com a mesma descrição
        public bool DuplicateCategory(int id, string description)
        {
            // Busca outra categoria
            var category = _context.Categories.Where(x => x.Id != id && x.Description == description).FirstOrDefault();

            // Se encontrar uma categoria com a mesma descrição retorna erro
            if (category != null)
                throw new Exception("Já existe uma categoria com a descrição informada!");

            return false;
        }
    }
}