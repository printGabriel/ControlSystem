using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        // serviço de aplicação responsável pelas regras de usuário
        private readonly IUserAppService _appService;

        public UserController(IUserAppService appService)
        {
            // injeção de dependência do serviço
            _appService = appService;
        }

        // cria um novo usuário
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto command)
        {
            try
            {
                // chama a camada de aplicação para criar o usuário
                var userDto = await _appService.CreateUser(command);

                // caso algo dê errado na criação
                if (userDto == null)
                    return BadRequest();

                return Ok(userDto);
            }
            catch (Exception e)
            {
                // captura erros da aplicação e retorna para o front
                return BadRequest(e.Message);
            }
        }

        // busca um usuário específico pelo id
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _appService.GetUserById(id);

            // se não encontrar o usuário
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // retorna todos os usuários cadastrados
        [HttpGet]
        public IActionResult GetUsers()
        {
            var user = _appService.GetUsers();

            // caso não exista nenhum usuário
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // atualiza os dados de um usuário
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto command)
        {
            // valida se o id da rota é o mesmo do corpo
            if (id != command.Id)
                return BadRequest("Id diferente do corpo.");

            try
            {
                var user = await _appService.UpdateUser(command);

                // caso o usuário não exista
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception e)
            {
                // retorna erro para o front caso algo falhe na atualização
                return BadRequest(e.Message);
            }
        }

        // remove um usuário pelo id
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var deleted = _appService.DeleteUserById(id);

            // se não conseguir deletar (ex: usuário não existe)
            if (!deleted)
                return NotFound();

            // retorno padrão para delete bem sucedido
            return NoContent();
        }

        // retorna o resumo financeiro geral do sistema
        [HttpGet("financial-summary")]
        public async Task<IActionResult> GetFinancialSummary()
        {
            var summary = await _appService.GetFinancialSummary();

            // caso não exista resumo
            if (summary == null)
                return NotFound();

            return Ok(summary);
        }
    }
}