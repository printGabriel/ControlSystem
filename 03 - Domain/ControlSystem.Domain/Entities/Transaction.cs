using ControlSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ControlSystem.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public decimal Value { get; private set; } = 0;
        public TransactionType TransactionType { get; private set; }
        public int CategoryId { get; private set; }
        public int UserId { get; private set; }

        public virtual User User { get; private set; }
        public virtual Category Category { get; private set; }

        public Transaction(string description, decimal value, TransactionType transactionType, int categoryId, int userId)
        {
            Description = description;
            Value = value; 
            TransactionType = transactionType;
            CategoryId = categoryId;
            UserId = userId;
        }

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
