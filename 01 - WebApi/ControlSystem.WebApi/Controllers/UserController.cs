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

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto command)
        {
            var userDto = await _appService.CreateUser(command);

            if (userDto == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _appService.GetUserById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto command)
        {
            if (id != command.Id)
                return BadRequest("Id diferente do corpo.");

            var user = await _appService.UpdateUser(command);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpDelete("{id}")]
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