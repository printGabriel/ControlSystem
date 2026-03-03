using ControlSystem.Application.DTOs;
using System.Threading.Tasks;

namespace ControlSystem.Application.Interfaces
{
    public interface ITransactionAppService
    {
        TransactionDto CreateTransaction(TransactionDto command);
        TransactionDto? GetTransactionById(int transactionId);
        Task<TransactionDto?> UpdateTransaction(TransactionDto command);
        bool DeleteTransactionById(int transactionId);
    }
}
