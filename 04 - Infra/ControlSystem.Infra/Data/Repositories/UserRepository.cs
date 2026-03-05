using ControlSystem.Application.DTOs;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Enums;
using ControlSystem.Domain.Interfaces;
using ControlSystem.Domain.Projections;
using ControlSystem.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ControlSystem.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User user)
        {
            DuplicateEmail(user.Email, user.Id);


            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public User Get(int id)
        {
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            
            return user;
        }

        public List<User> GetAll()
        {
            var user = _context.Users.ToList();

            return user;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public bool Delete(int id)
        {
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            var userTransactions = _context.Transactions.Where(x => x.UserId == id).ToList();

            if (user != null)
            {
                _context.Remove(user);

                if(userTransactions != null)
                {
                    _context.RemoveRange(userTransactions);
                }

                _context.SaveChanges();
            }

            return true;
        }

        public async Task<List<UserFinancialSummary>> GetFinancialSummary()
        {
            using (var ct = _context)
            {
                var summary = (from u in ct.Users
                               join t in ct.Transactions on u.Id equals t.UserId into tQuery

                               select new UserFinancialSummary
                               {
                                   UserId = u.Id,
                                   UserName = u.Name,
                                   TotalIncome = tQuery
                                      .Where(t => t.TransactionType == TransactionType.Income)
                                      .Sum(t => (decimal?)t.Value) ?? 0,

                                   TotalExpense = tQuery
                                      .Where(t => t.TransactionType == TransactionType.Expense)
                                      .Sum(t => (decimal?)t.Value) ?? 0,
                               }).ToListAsync();

                return await summary;
            }
        }

        public bool DuplicateEmail(string email, int id)
        {
            var user = _context.Users.Where(x => x.Id != id && x.Email == email).FirstOrDefaultAsync();

            if (user != null)
                throw new Exception("Já existe um usuário com o e-mail informado!");

            return false;
        }
    }
}
