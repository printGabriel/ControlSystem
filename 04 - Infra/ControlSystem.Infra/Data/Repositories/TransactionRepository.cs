using System;
using System.Collections.Generic;
using System.Text;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Interfaces;
using ControlSystem.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public Transaction Get(int id)
        {
            var transaction = _context.Transactions.Where(x => x.Id == id).FirstOrDefault();

            return transaction;
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
