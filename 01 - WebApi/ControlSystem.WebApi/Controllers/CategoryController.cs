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
            try
            {
                var categoryDto = await _appService.CreateCategory(command);

                if (categoryDto == null)
                {
                    return NotFound();
                }

                return Ok(categoryDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-category-by-id/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _appService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpGet("get-all-categories")]
        public IActionResult GetAllCategories()
        {
            var category = _appService.GetAllCategories();

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPut("update-category-by-id/{id}")]
        public async Task<IActionResult> UpdateCategoryById(int id, [FromBody] CategoryDto command)
        {
            if (id != command.Id)
                return BadRequest("Id da rota diferente do corpo da requisição.");

            try
            {
                var category = await _appService.UpdateCategory(command);

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategoryById(int id)
        {
            var deleted = _appService.DeleteCategoryById(id);

            if (deleted == false)
            {
                return NotFound();
            }

            return Ok(deleted);
        }
    }
}