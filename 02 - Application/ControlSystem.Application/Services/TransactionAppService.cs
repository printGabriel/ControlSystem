using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Interfaces;
using System.Threading.Tasks;

namespace ControlSystem.Application.Services
{
    public class TransactionAppService : ITransactionAppService
    {
        private readonly ITransactionRepository _repository;

        public TransactionAppService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public TransactionDto CreateTransaction(TransactionDto command)
        {
            var transaction = new Transaction(
                command.Description,
                command.Value,
                command.TransactionType,
                command.CategoryId,
                command.UserId
            );

            _repository.Add(transaction);

            return command;
        }

        public TransactionDto? GetTransactionById(int transactionId)
        {
            var transaction = _repository.Get(transactionId);

            if (transaction == null)
                return null;

            return new TransactionDto
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Value = transaction.Value,
                TransactionType = transaction.TransactionType,
                CategoryId = transaction.CategoryId,
                UserId = transaction.UserId
            };
        }

        public async Task<TransactionDto?> UpdateTransaction(TransactionDto command)
        {
            var transaction = _repository.Get(command.Id);

            if (transaction == null)
                return null;

            transaction.Update(
                command.Description,
                command.Value,
                command.TransactionType,
                command.CategoryId,
                command.UserId
            );

            await _repository.Save();

            return new TransactionDto
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Value = transaction.Value,
                TransactionType = transaction.TransactionType,
                CategoryId = transaction.CategoryId,
                UserId = transaction.UserId
            };
        }

        public bool DeleteTransactionById(int transactionId)
        {
            var deleted = _repository.Delete(transactionId);

            if (deleted == false)
                return false;

            return true;
        }
    }
}