using ControlSystem.Application.DTOs;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Enums;
using ControlSystem.Domain.Interfaces;
using ControlSystem.Domain.Projections;
using ControlSystem.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlSystem.Infra.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationContext _context;

        public TransactionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Transaction> Add(Transaction transaction)
        {
            /* Para não furar a responsabilidade única, adicionei a verificação de idade na entidade User.
               Dessa forma, apenas faço a chamada e valido junto ao tipo de transação*/

            var user = await _context.Users.Where(x => x.Id == transaction.UserId).FirstOrDefaultAsync();

            if (user != null && !user.IsAdult(user.BirthDate) && transaction.TransactionType != TransactionType.Expense)
            {
                throw new Exception("Usuário deve ser maior de 18 anos.");
            }

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public Transaction Get(int id)
        {
            var transaction = _context.Transactions.Where(x => x.Id == id).FirstOrDefault();

            return transaction;
        }

        public List<TransactionProjection> GetAll()
        {
            using (var ct = _context)
            {
                var transaction = (from t in ct.Transactions
                                   join c in ct.Categories on t.CategoryId equals c.Id
                                   join u in ct.Users on t.UserId equals u.Id

                                   select new TransactionProjection
                                   {
                                       Id = t.Id,
                                       Description = t.Description,
                                       Value = t.Value,
                                       TransactionType = (int)t.TransactionType,
                                       CategoryId = t.CategoryId,
                                       CategoryName = c.Description,
                                       UserId = t.UserId,
                                       UserName = u.Name,
                                   }).ToList();

                return transaction;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public bool Delete(int id)
        {
            var transaction = _context.Transactions.Where(x => x.Id == id).FirstOrDefault();

            if (transaction != null)
            {
                _context.Remove(transaction);
                _context.SaveChanges();
            }

            return true;
        }
    }
}
