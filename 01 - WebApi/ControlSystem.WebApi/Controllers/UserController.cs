using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _appService;

        public UserController(IUserAppService appService)
        {
            _appService = appService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto command)
        {
            try
            {
                var userDto = await _appService.CreateUser(command);

                if (userDto == null)
                    return BadRequest();

                return Ok(userDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-user-by-id/{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _appService.GetUserById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("get-all-users")]
        public IActionResult GetUsers()
        {
            var user = _appService.GetUsers();

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("update-user-by-id/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto command)
        {
            if (id != command.Id)
                return BadRequest("Id diferente do corpo.");

            try
            {
                var user = await _appService.UpdateUser(command);

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete-user-by-{id}")]
        public IActionResult DeleteUser(int id)
        {
            var deleted = _appService.DeleteUserById(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("financial-summary")]
        public async Task<IActionResult> GetFinancialSummary()
        {
            var summary = await _appService.GetFinancialSummary();

            if (summary == null)
                return NotFound();

            return Ok(summary);
        }
    }
}