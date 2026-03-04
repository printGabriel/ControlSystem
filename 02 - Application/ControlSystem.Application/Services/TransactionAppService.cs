using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Enums;
using ControlSystem.Domain.Interfaces;
using System.Threading.Tasks;

namespace ControlSystem.Application.Services
{
    public class TransactionAppService : ITransactionAppService
    {
        private readonly ITransactionRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public TransactionAppService(ITransactionRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<TransactionDto> CreateTransaction(TransactionDto command)
        {
            var category = _categoryRepository.Get(command.CategoryId);

            if (category == null)
                throw new Exception("Categoria não encontrada.");


            if (category.PurposeType != PurposeType.Both && command.TransactionType != (int)category.PurposeType)
                throw new Exception("Categoria incompatível.");

            var transaction = new Transaction(
                command.Description,
                command.Value,
                (TransactionType)command.TransactionType,
                command.CategoryId,
                command.UserId
            );

            await _repository.Add(transaction);

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
                TransactionType = (int)transaction.TransactionType,
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
                (TransactionType)command.TransactionType,
                command.CategoryId,
                command.UserId
            );

            await _repository.Save();

            return new TransactionDto
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Value = transaction.Value,
                TransactionType = (int)transaction.TransactionType,
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