using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Projections;

namespace ControlSystem.Domain.Interfaces
{
    // Interface responsável pelas operações de acesso aos dados de usuários
    // aqui ficam apenas as assinaturas dos métodos que manipulam usuários no banco
    public interface IUserRepository
    {
        // Adiciona um novo usuário no banco
        Task<User> Add(User user);

        // Busca um usuário pelo Id
        User Get(int id);

        // Retorna todos os usuários cadastrados
        List<User> GetAll();

        // Salva alterações pendentes no banco
        Task Save();

        // Remove um usuário pelo Id
        bool Delete(int id);

        // Retorna um resumo financeiro de todos os usuários
        Task<List<UserFinancialSummary>> GetFinancialSummary();

        // Verifica se já existe outro usuário com o mesmo e-mail
        bool DuplicateEmail(string email, int id);
    }
}