using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Projections;
using System.Threading.Tasks;

namespace ControlSystem.Domain.Interfaces
{
    // Interface responsável pelo acesso aos dados de transações no banco
    public interface ITransactionRepository
    {
        // Adiciona uma nova transação
        Task<Transaction> Add(Transaction transaction);

        // Busca uma transação pelo Id
        Transaction Get(int id);

        // Retorna todas as transações (utilizando classe de leitura)
        List<TransactionProjection> GetAll();

        // Salva alterações pendentes no banco
        Task Save();

        // Remove uma transação pelo Id
        bool Delete(int id);
    }
}
