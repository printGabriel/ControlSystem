using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Interfaces;

namespace ControlSystem.Application.Services
{
    public class UserAppService : IUserAppService
    {
        // Repositório responsável por acessar os dados de usuário no banco
        private readonly IUserRepository _repository;

        public UserAppService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> CreateUser(UserDto command)
        {
            // Cria a entidade de usuário com os dados da DTO
            var user = new User(command.Name, command.Email, command.BirthDate);

            // Salva no banco através do repositório
            await _repository.Add(user);

            // Retorna a DTO criado
            return command;
        }

        public UserDto GetUserById(int userId)
        {
            // Busca o usuário pelo id
            var user = _repository.Get(userId);

            if (user == null)
            {
                return null;
            }

            // Converte a entidade para DTO antes de retornar
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                BirthDate = user.BirthDate
            };
        }

        public List<UserDto> GetUsers()
        {
            // Busca todos os usuários no repositório
            var users = _repository.GetAll();

            // Se não existir nenhum usuário retorna lista vazia
            if (!users.Any())
            {
                return new List<UserDto>();
            }

            var usersDto = new List<UserDto>();

            // Converte cada entidade para UserDto
            foreach (var u in users)
            {
                usersDto.Add(new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    BirthDate = u.BirthDate
                });
            }

            return usersDto;
        }

        public async Task<UserDto?> UpdateUser(UserDto command)
        {
            // Busca o usuário atual no banco
            var user = _repository.Get(command.Id);

            // Verifica se já existe outro usuário com o mesmo email
            _repository.DuplicateEmail(command.Email, command.Id);

            if (user == null)
                return null;

            // Atualiza os dados do usuário com os novos valores do DTO
            user.Update(command.Name, command.Email, command.BirthDate);

            // Salva as alterações no banco
            await _repository.Save();

            // Retorna o usuário atualizado convertido para UserDto
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                BirthDate = user.BirthDate
            };
        }

        public bool DeleteUserById(int userId)
        {
            // Remove o usuário pelo id
            var user = _repository.Delete(userId);

            if (user == false)
                return false;

            return true;
        }

        public async Task<FinancialSummaryResponse> GetFinancialSummary()
        {
            // Busca os dados no repositório
            var summary = await _repository.GetFinancialSummary();

            var response = new FinancialSummaryResponse();

            // Monta a lista do sumário por usuário
            response.Users = summary.Select(s => new SummaryDto
            {
                UserId = s.UserId,
                UserName = s.UserName,
                TotalIncome = s.TotalIncome,
                TotalExpense = s.TotalExpense,
                Balance = s.TotalIncome - s.TotalExpense
            }).ToList();

            // Calcula os totais gerais
            response.TotalIncome = summary.Sum(s => s.TotalIncome);
            response.TotalExpense = summary.Sum(s => s.TotalExpense);
            response.TotalBalance = response.TotalIncome - response.TotalExpense;

            return response;
        }
    }
}