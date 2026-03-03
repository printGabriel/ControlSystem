using ControlSystem.Domain.Entities;
using System.Threading.Tasks;

namespace ControlSystem.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> Add(Transaction transaction);
        Transaction Get(int id);
        Task Save();
        bool Delete(int id);
    }
}
