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
        private readonly ICategoryRepository _repository;

        public CategoryAppService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoryDto> CreateCategory(CategoryDto command)
        {
            var category = new Category(command.Description, (PurposeType)command.PurposeType);

            await _repository.Add(category);

            return command;
        }

        public CategoryDto? GetCategoryById(int categoryId)
        {
            var category = _repository.Get(categoryId);

            if (category == null)
                return null;

            return new CategoryDto
            {
                Id = category.Id,
                Description = category.Description,
                PurposeType = (int)category.PurposeType
            };
        }
        public List<CategoryDto> GetAllCategories()
        {
            var categories = _repository.GetAll();

            if (!categories.Any())
                return null;

            var categoryDto = new List<CategoryDto>();

            foreach (var c in categories)
            {
                categoryDto.Add(new CategoryDto
                {
                    Id = c.Id,
                    Description = c.Description,
                    PurposeType = (int)c.PurposeType
                });
            }
            ;

            return categoryDto;
        }

        public async Task<CategoryDto?> UpdateCategory(CategoryDto command)
        {
            var category = _repository.Get(command.Id);

            if (category == null)
                return null;

            category.Update(command.Description, (PurposeType)command.PurposeType);

            await _repository.Save();

            return new CategoryDto
            {
                Id = category.Id,
                Description = category.Description,
                PurposeType = (int)category.PurposeType
            };
        }

        public bool DeleteCategoryById(int categoryId)
        {
            var deleted = _repository.Delete(categoryId);

            if (deleted == false)
                return false;

            return true;
        }
    }
}