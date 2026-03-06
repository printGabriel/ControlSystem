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
    // Repositório responsável por acessar os dados de transações no banco, usando entity framework
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationContext _context;

        // Injeção de dependência do contexto do banco para acessar os dados
        public TransactionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Transaction> Add(Transaction transaction)
        {
            /* 
               Para não quebrar a responsabilidade única,
               a verificação de idade está na entidade User.
               Aqui apenas consulto o usuário e aplicamos a regra.
            */

            var user = await _context.Users.Where(x => x.Id == transaction.UserId).FirstOrDefaultAsync();

            // Usuários menores de idade só podem registrar despesas
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
                // Consulta para retornar informações da transação junto com nome da categoria e do usuário
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
            // método utilizado para salvar updates no banco
            await _context.SaveChangesAsync();
        }

        public bool Delete(int id)
        {
            // Busca pela transação no banco
            var transaction = _context.Transactions.Where(x => x.Id == id).FirstOrDefault();

            // se encontrar remove e salva
            if (transaction != null)
            {
                _context.Remove(transaction);
                _context.SaveChanges();
            }

            return true;
        }
    }
