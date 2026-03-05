using ControlSystem.Application.DTOs;
using System.Threading.Tasks;

namespace ControlSystem.Application.Interfaces
{
    public interface ITransactionAppService
    {
        Task<TransactionDto> CreateTransaction(TransactionDto command);
        TransactionDto? GetTransactionById(int transactionId);
        List<TransactionDto?> GetTransactions();
        Task<TransactionDto?> UpdateTransaction(TransactionDto command);
        bool DeleteTransactionById(int transactionId);
    }
}
