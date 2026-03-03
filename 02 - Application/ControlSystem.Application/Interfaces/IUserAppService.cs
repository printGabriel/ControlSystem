using ControlSystem.Application.DTOs;
using ControlSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlSystem.Application.Interfaces
{
    public interface IUserAppService
    {
        UserDto CreateUser(UserDto command);
        UserDto? GetUserById(int userId);
        Task<UserDto?> UpdateUser(UserDto command);
        bool DeleteUserById(int userId);

    }
}
