using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace ControlSystem.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateOnly BirthDate { get; private set; }

        public User(string name, string email, DateOnly birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }
        public void Update(string name, string email, DateOnly birthDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("A propriedade \"Nome\", não pode estar vazia!");
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("A propriedade \"E-mail\", não pode estar vazia!");

            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public bool IsAdult(DateOnly birthDate)
        {
            if (birthDate > DateOnly.FromDateTime(DateTime.Today).AddYears(-18))
            {
                return false;
            }
            else
                return true;
        }
    }
}