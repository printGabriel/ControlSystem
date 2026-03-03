using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystem.WebApi.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryAppService _appService;

        public CategoryController(ICategoryAppService appService)
        {
            _appService = appService;
        }

        [Route("api/category/createCategory")]
        [HttpPost]
        public IActionResult CreateCategory(CategoryDto command)
        {
            var categoryDto = _appService.CreateCategory(command);

            if (categoryDto == null)
            {
                return NotFound();
            }

            return Ok(categoryDto);
        }

        [Route("api/category/getCategoryById")]
        [HttpGet]
        public IActionResult GetCategoryById(int categoryId)
        {
            var category = _appService.GetCategoryById(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [Route("api/category/updateCategory")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategoryById(int id, [FromBody] CategoryDto command)
        {
            if (id != command.Id)
                return BadRequest("Id da rota diferente do corpo da requisição.");

            var category = await _appService.UpdateCategory(command);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [Route("api/category/deleteCategoryById")]
        [HttpGet]
        public IActionResult DeleteCategoryById(int categoryId)
        {
            var deleted = _appService.DeleteCategoryById(categoryId);

            if (deleted == false)
            {
                return NotFound();
            }

            return Ok(deleted);
        }
    }
}