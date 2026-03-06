using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace ControlSystem.Domain.Entities
{
    public class User
    {
        //Id do usuário, gerado automaticamente pelo banco de dados
        public int Id { get; private set; }

        //nome do usuário
        public string Name { get; private set; }

        //email do usuário
        public string Email { get; private set; }
        
        //aqui optei por data de nascimento ao invés de idade, acho que fica melhor em quesito de atualização
        public DateOnly BirthDate { get; private set; }

        //construtor para criar um novo usuário
        public User(string name, string email, DateOnly birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        //método para atualizar os dados do usuário, com validação para campos obrigatórios
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

        //método para validar se o usuário é maior de idade, usado na application antes de criar ou atualizar o usuário
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