using ControlSystem.Application.DTOs;
using ControlSystem.Application.Interfaces;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlSystem.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _repository;

        public UserAppService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> CreateUser(UserDto command)
        {
            var user = new User(command.Name, command.Email, command.BirthDate);

            await _repository.Add(user);

            return command;
        }

        public UserDto GetUserById(int userId)
        {
            var user = _repository.Get(userId);

            if (user == null)
            {
                return null;
            }

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
            var users = _repository.GetAll();

            if (!users.Any())
            {
                return new List<UserDto>();
            }

            var usersDto = new List<UserDto>();

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
            var user = _repository.Get(command.Id);

            if (user == null)
                return null;

            user.Update(command.Name, command.Email, command.BirthDate);

            await _repository.Save();

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
            var user = _repository.Delete(userId);

            if (user == false)
                return false;

            return true;
        }

        public async Task<FinancialSummaryResponse> GetFinancialSummary()
        {
            var summary = await _repository.GetFinancialSummary();

            var response = new FinancialSummaryResponse();

            response.Users = summary.Select(s => new SummaryDto
            {
                UserId = s.UserId,
                UserName = s.UserName,
                TotalIncome = s.TotalIncome,
                TotalExpense = s.TotalExpense,
                Balance = s.Balance
            }).ToList();

            response.TotalIncome = summary.Sum(s => s.TotalIncome);
            response.TotalExpense = summary.Sum(s => s.TotalExpense);
            response.TotalBalance = response.TotalIncome - response.TotalExpense;

            return response;
        }
    }
}