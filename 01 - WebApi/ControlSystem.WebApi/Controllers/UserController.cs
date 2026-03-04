using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystem.WebApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _appService;

        public UserController(IUserAppService appService)
        {
            _appService = appService;
        }

        [HttpPost("api/user/createUser")]
        public async Task<IActionResult> CreateUser(UserDto command)
        { 
            var userDto = await _appService.CreateUser(command);

            if (userDto == null) 
            {
                return NotFound();
            }

            return Ok(userDto);

        }

        [HttpGet("api/user/getUserById")]
        public IActionResult GetUserById(int userId)
        {
            var user = _appService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);

        }

        [HttpPut("api/user/updateUser")]
        public async Task<IActionResult>UpdateUserById(int id, [FromBody]UserDto command)
        {
            if (id != command.Id)
                return BadRequest("Id da rota diferente do corpo da requisição.");

            var user = await _appService.UpdateUser(command);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);

        }

        [HttpGet("api/user/deleteUserById")]
        public IActionResult DeleteUserById(int userId)
        {
            var deleted = _appService.DeleteUserById(userId);

            if (deleted == false)
            {
                return NotFound();
            }

            return Ok(deleted);

        }

        [HttpGet("financial-summary")]
        public async Task<IActionResult> GetFinancialSummary()
        {
            var sumarry = await _appService.GetFinancialSummary();

            if (sumarry == null)
            {
                return NotFound();
            }
             
            return Ok(sumarry);
        }
    }
}
