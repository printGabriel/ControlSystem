using ControlSystem.Application.DTOs;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Projections;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlSystem.Application.Interfaces
{
    // Interface responsável pelas operações relacionadas aos usuários na camada de aplicação.
    public interface IUserAppService
    {
        // Cria um novo usuário no sistema
        Task<UserDto> CreateUser(UserDto command);

        // Retorna um usuário específico pelo Id
        UserDto? GetUserById(int userId);

        // Retorna a lista de todos os usuários cadastrados
        List<UserDto?> GetUsers();

        // Atualiza os dados de um usuário existente
        Task<UserDto?> UpdateUser(UserDto command);

        // Remove um usuário pelo Id
        bool DeleteUserById(int userId);

        // Retorna um resumo financeiro geral do sistema
        Task<FinancialSummaryResponse> GetFinancialSummary();
    }
}
