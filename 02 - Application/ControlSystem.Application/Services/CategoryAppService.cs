using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Enums;
using ControlSystem.Domain.Interfaces;
using System.Threading.Tasks;

namespace ControlSystem.Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        // Repositório responsável pelo acesso aos dados das categorias
        private readonly ICategoryRepository _repository;

        public CategoryAppService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoryDto> CreateCategory(CategoryDto command)
        {
            // Cria a entidade de domínio com os dados da dto
            var category = new Category(command.Description, (PurposeType)command.PurposeType);

            await _repository.Add(category);

            return command;
        }

        // busca a categoria pelo id
        public CategoryDto? GetCategoryById(int categoryId)
        {
            var category = _repository.Get(categoryId);

            if (category == null)
                return null;

            // Converte a entidade para DTO
            return new CategoryDto
            {
                Id = category.Id,
                Description = category.Description,
                PurposeType = (int)category.PurposeType
            };
        }

        //busca por todas as categorias
        public List<CategoryDto> GetAllCategories()
        {
            var categories = _repository.GetAll();

            if (!categories.Any())
                return new List<CategoryDto>();

            var categoryDto = new List<CategoryDto>();

            // Converte a lista de entidades para DTO
            foreach (var c in categories)
            {
                categoryDto.Add(new CategoryDto
                {
                    Id = c.Id,
                    Description = c.Description,
                    PurposeType = (int)c.PurposeType,
                });
            };

            return categoryDto;
        }

        public async Task<CategoryDto?> UpdateCategory(CategoryDto command)
        {
            // Verifica se já existe categoria com a mesma descrição e já retorna erro caso encontre
            var duplicate = _repository.DuplicateCategory(command.Id, command.Description);

            var category = _repository.Get(command.Id);

            //caso não encontre a categoria
            if (category == null)
                return null;

            // Atualiza os dados da entidade
            category.Update(command.Description, (PurposeType)command.PurposeType);

            await _repository.Save();

            return new CategoryDto
            {
                Id = category.Id,
                Description = category.Description,
                PurposeType = (int)category.PurposeType
            };
        }

        //remove a categoria pelo seu id
        public bool DeleteCategoryById(int categoryId)
        {
            var deleted = _repository.Delete(categoryId);

            if (deleted == false)
                return false;

            return true;
        }
    }
}