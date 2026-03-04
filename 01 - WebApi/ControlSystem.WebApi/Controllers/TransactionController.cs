using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionAppService _appService;

        public TransactionController(ITransactionAppService appService)
        {
            _appService = appService;
        }

        [HttpPost("create-transaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDto command)
        {
            var transactionDto = await _appService.CreateTransaction(command);

            if (transactionDto == null)
            {
                return NotFound();
            }

            return Ok(transactionDto);
        }

        [HttpGet("get-transaction-by-{id}")]
        public IActionResult GetTransactionById(int transactionId)
        {
            var transaction = _appService.GetTransactionById(transactionId);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPut("update-transaction-by-{id}")]
        public async Task<IActionResult> UpdateTransactionById(int id, [FromBody] TransactionDto command)
        {
            if (id != command.Id)
                return BadRequest("Id da rota diferente do corpo da requisição.");

            var transaction = await _appService.UpdateTransaction(command);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpDelete("delete-transaction-by-{id}")]
        public IActionResult DeleteTransactionById(int transactionId)
        {
            var deleted = _appService.DeleteTransactionById(transactionId);

            if (deleted == false)
            {
                return NotFound();
            }

            return Ok(deleted);
        }
    }
}