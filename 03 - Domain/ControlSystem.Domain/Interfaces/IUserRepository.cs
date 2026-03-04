using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Projections;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlSystem.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        User Get(int id);
        Task Save();
        bool Delete(int id);
        Task<List<UserFinancialSummary>> GetFinancialSummary();
    }
}
