using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/categories/")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryAppService _appService;

        public CategoryController(ICategoryAppService appService)
        {
            _appService = appService;
        }

        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory(CategoryDto command)
        {
            var categoryDto = await _appService.CreateCategory(command);

            if (categoryDto == null)
            {
                return NotFound();
            }

            return Ok(categoryDto);
        }

        [HttpGet("get-category-by-{id}")]
        public IActionResult GetCategoryById(int categoryId)
        {
            var category = _appService.GetCategoryById(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPut("update-category-by-{id}")]
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

        [HttpGet("delete-category-by-{id}")]
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