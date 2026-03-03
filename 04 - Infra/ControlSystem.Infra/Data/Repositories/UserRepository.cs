using ControlSystem.Application.DTOs;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Interfaces;
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
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public User Get(int id)
        {
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();

            //tratar exceptions
            return user;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public bool Delete(int id)
        {
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();

            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
            }

            //tratar exceptions
            return true;
        }
    }
}
