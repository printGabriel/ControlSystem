using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/categories/")]
    public class CategoryController : ControllerBase
    {
        // serviço de aplicação responsável pelas regras das categorias
        private readonly ICategoryAppService _appService;

        public CategoryController(ICategoryAppService appService)
        {
            // injeção de dependência do serviço
            _appService = appService;
        }

        //cria uma nova categoria
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDto command)
        {
            try
            {
                var categoryDto = await _appService.CreateCategory(command);

                // caso algum erro aconteça e não retorne a categoria
                if (categoryDto == null)
                {
                    return NotFound();
                }

                //retorna a categoria criada
                return Ok(categoryDto);
            }
            catch (Exception e)
            {
                //caso alguma validação retorne algum erro
                return BadRequest(e.Message);
            }
        }

        //busca por uma categoria específica pelo id
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _appService.GetCategoryById(id);

            //caso não exista
            if (category == null)
            {
                return NotFound();
            }

            //retorna a buscada
            return Ok(category);
        }

        //busca por todas as categorias
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var category = _appService.GetAllCategories();

            //se não encontrar, retorna erro
            if (!category.Any())
                return NotFound();

            //retorno da listagem de categorias
            return Ok(category);
        }

        //atualiza uma categoria específica pelo id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryById(int id, [FromBody] CategoryDto command)
        {
            // Verifica se o Id da rota é o mesmo do objeto
            if (id != command.Id)
                return BadRequest("Id da rota diferente do corpo da requisição.");

            try
            {
                var category = await _appService.UpdateCategory(command);

                //caso não encontre a categoria
                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch (Exception e)
            {
                // retorna erro para o front caso algo falhe na atualização
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        //remove uma categoria específica pelo id
        public IActionResult DeleteCategoryById(int id)
        {
            var deleted = _appService.DeleteCategoryById(id);

            //caso não encontre, retorna erro
            if (deleted == false)
            {
                return NotFound();
            }

            // retorno de sucesso no delete
            return NoContent();
        }
    }
}