using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        // Serviço responsável por executar as regras de negócio das transações
        private readonly ITransactionAppService _appService;

        public TransactionController(ITransactionAppService appService)
        {
            _appService = appService;
        }

        // Endpoint responsável por criar uma nova transação
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDto command)
        {
            try
            {
                var transactionDto = await _appService.CreateTransaction(command);

                // Caso algo dê errado e não retorne a transação criada
                if (transactionDto == null)
                {
                    return NotFound();
                }

                // Retorna a transação criada
                return Ok(transactionDto);
            }
            catch (Exception e)
            {
                // Caso alguma regra de negócio lance erro
                return BadRequest(e.Message);
            }
        }

        // Busca uma transação específica pelo Id
        [HttpGet("{id}")]
        public IActionResult GetTransactionById(int id)
        {
            var transaction = _appService.GetTransactionById(id);

            // Caso a transação não exista
            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // Retorna todas as transações cadastradas
        [HttpGet]
        public IActionResult GetTransactions()
        {
            var transaction = _appService.GetAllTransactions();

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // Atualiza uma transação existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransactionById(int id, [FromBody] TransactionDto command)
        {
            // Validação simples para garantir que o id da rota é o mesmo do objeto
            if (id != command.Id)
                return BadRequest("Id da rota diferente do corpo da requisição.");

            try
            {
                var transaction = await _appService.UpdateTransaction(command);

                if (transaction == null)
                {
                    return NotFound();
                }

                return Ok(transaction);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Remove uma transação pelo Id
        [HttpDelete("{id}")]
        public IActionResult DeleteTransactionById(int id)
        {
            var deleted = _appService.DeleteTransactionById(id);

            // Caso a transação não exista
            if (!deleted)
                return NotFound();

            // Retorno para exclusão bem sucedida
            return NoContent();
        }
    }
}