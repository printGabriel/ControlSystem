using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystem.WebApi.Controllers
{
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionAppService _appService;

        public TransactionController(ITransactionAppService appService)
        {
            _appService = appService;
        }

        [Route("api/transaction/createTransaction")]
        [HttpPost]
        public async Task<IActionResult> CreateTransaction(TransactionDto command)
        {
            var transactionDto = await _appService.CreateTransaction(command);

            if (transactionDto == null)
            {
                return NotFound();
            }

            return Ok(transactionDto);
        }

        [Route("api/transaction/getTransactionById")]
        [HttpGet]
        public IActionResult GetTransactionById(int transactionId)
        {
            var transaction = _appService.GetTransactionById(transactionId);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [Route("api/transaction/updateTransaction")]
        [HttpPut]
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

        [Route("api/transaction/deleteTransactionById")]
        [HttpGet]
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