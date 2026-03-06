using ControlSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ControlSystem.Domain.Entities
{
    public class Transaction
    {
        //Id da transação
        public int Id { get; private set; }

        //Descrição da transação
        public string Description { get; private set; } = string.Empty;

        //valor da transação
        public decimal Value { get; private set; } = 0;

        //tipo de transação referenciado por um enum
        public TransactionType TransactionType { get; private set; }

        //id da categoria que referencia a trransação
        public int CategoryId { get; private set; }

        //id do usuário que fez a transação
        public int UserId { get; private set; }

        // propriedades de navegação para os relacionamentos com User e Category
        public virtual User User { get; private set; }
        public virtual Category Category { get; private set; }

        // construtor para criar uma nova transação
        public Transaction(string description, decimal value, TransactionType transactionType, int categoryId, int userId)
        {
            Description = description;
            Value = value; 
            TransactionType = transactionType;
            CategoryId = categoryId;
            UserId = userId;
        }

        // método para atualizar uma transação existente
        public void Update(string description, decimal value, TransactionType transactionType, int categoryId, int userId)
        {
            Description = description;
            Value = value;
            TransactionType = transactionType;
            CategoryId = categoryId;
            UserId = userId;
        }
    }
}
