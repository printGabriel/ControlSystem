using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Projections;
using System.Threading.Tasks;

namespace ControlSystem.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> Add(Transaction transaction);
        Transaction Get(int id);
        List<TransactionProjection> GetAll();
        Task Save();
        bool Delete(int id);
    }
}
