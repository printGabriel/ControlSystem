using ControlSystem.Application.DTOs;

namespace ControlSystem.Application.Interfaces
{
    // Interface responsável pelas operações relacionadas as transalções na camada de aplicação.
    public interface ITransactionAppService
    {
        // Cria uma nova transação
        Task<TransactionDto> CreateTransaction(TransactionDto command);

        // Busca uma transação pelo Id
        TransactionDto? GetTransactionById(int transactionId);

        // Retorna todas as transações cadastradas
        List<TransactionDto?> GetAllTransactions();

        // Atualiza uma transação existente
        Task<TransactionDto?> UpdateTransaction(TransactionDto command);

        // Remove uma transação pelo Id
        bool DeleteTransactionById(int transactionId);
    }
}
