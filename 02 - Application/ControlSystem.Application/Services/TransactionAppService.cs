using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Enums;
using ControlSystem.Domain.Interfaces;

public class TransactionAppService : ITransactionAppService
{
    // Repositório de transações
    private readonly ITransactionRepository _repository;

    // Repositório de categorias (uso para validar compatibilidade)
    private readonly ICategoryRepository _categoryRepository;

    public TransactionAppService(ITransactionRepository repository, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
    }

    public async Task<TransactionDto> CreateTransaction(TransactionDto command)
    {
        // Busca a categoria para validar a transação
        var category = _categoryRepository.Get(command.CategoryId);

        if (category == null)
            throw new Exception("Categoria não encontrada.");

        // Verifica se a transação é compatível com o tipo da categoria
        if (category.PurposeType != PurposeType.Both && command.TransactionType != (int)category.PurposeType)
            throw new Exception("Categoria incompatível.");

        // Cria a entidade
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

        // Converte a entidade para DTO
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

    public List<TransactionDto> GetAllTransactions()
    {
        var transactions = _repository.GetAll();

        if (!transactions.Any())
            return new List<TransactionDto>();

        var transactionsDto = new List<TransactionDto>();

        // Converte a classe de leitura para DTO
        foreach (var t in transactions)
        {
            transactionsDto.Add(new TransactionDto
            {
                Id = t.Id,
                Description = t.Description,
                Value = t.Value,
                CategoryId = t.CategoryId,
                TransactionType = t.TransactionType,
                CategoryName = t.CategoryName,
                UserId = t.UserId,
                UserName = t.UserName
            });
        }
        ;

        return transactionsDto;
    }

    public async Task<TransactionDto?> UpdateTransaction(TransactionDto command)
    {
        var category = _categoryRepository.Get(command.CategoryId);

        if (category == null)
            throw new Exception("Categoria não encontrada.");

        // Validação de compatibilidade entre categoria e tipo de transação
        if (category.PurposeType != PurposeType.Both && command.TransactionType != (int)category.PurposeType)
            throw new Exception("Categoria incompatível.");

        var transaction = _repository.Get(command.Id);

        if (transaction == null)
            return null;

        // Atualiza os dados da transação
        transaction.Update(
            command.Description,
            command.Value,
            (TransactionType)command.TransactionType,
            command.CategoryId,
            command.UserId
        );

        await _repository.Save();

        // Retorna o DTO atualizado
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