using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Enums;
using ControlSystem.Domain.Interfaces;
using ControlSystem.Domain.Projections;
using ControlSystem.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControlSystem.Infra.Repositories
{
    // Repositório responsável por acessar os dados de usuários no banco, usando entity framework
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        // Injeção de dependência do contexto do banco para acessar os dados
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        // Adiciona um novo usuário no banco, verificando se o e-mail já existe
        public async Task<User> Add(User user)
        {
            // verificação de email, passando id e email para evitar que o mesmo usuário seja considerado duplicado
            DuplicateEmail(user.Email, user.Id);

            //adiciona o usuário no banco e salva as alterações
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public User Get(int id)
        {
            // Busca o usuário pelo id
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();

            return user;
        }

        public List<User> GetAll()
        {
            // Busca por todos os usuários cadastros
            var user = _context.Users.ToList();

            return user;
        }

        public async Task Save()
        {
            // Salva no banco as alterações de update
            await _context.SaveChangesAsync();
        }

        public bool Delete(int id)
        {
            // Busca pelo usuário
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            // Busca pelas transações feitas pelo mesmo usuário
            var userTransactions = _context.Transactions.Where(x => x.UserId == id).ToList();

            //se encontrar o usuário
            if (user != null)
            {
                //se encontrar transações desse usuário, as remove primeiro pra evitar erros de fk
                if (userTransactions.Any())
                {
                    _context.RemoveRange(userTransactions);
                }

                // remove o usuário e salva as alterações
                _context.Remove(user);
                _context.SaveChanges();

                return true;
            }
            else
                return false;
        }

        //Método responsável por gerar o sumário financeiro dos usuários.
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

                                   //aqui soma as receitas
                                   TotalIncome = tQuery
                                      .Where(t => t.TransactionType == TransactionType.Income)
                                      .Sum(t => (decimal?)t.Value) ?? 0,

                                   //aqui soma as despesas
                                   TotalExpense = tQuery
                                      .Where(t => t.TransactionType == TransactionType.Expense)
                                      .Sum(t => (decimal?)t.Value) ?? 0,
                               }).ToListAsync();

                return await summary;
            }
        }

        public bool DuplicateEmail(string email, int id)
        {
            // verifica se já existe outro usuário com o mesmo e-mail ignorando o ele mesmo (no caso de update)
            var user = _context.Users.Where(x => x.Id != id && x.Email == email).FirstOrDefault();

            if (user != null)
                throw new Exception("Já existe um usuário com o e-mail informado!");

            return false;
        }
    }
}
