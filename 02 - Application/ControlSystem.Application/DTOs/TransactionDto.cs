using ControlSystem.Domain.Enums;
using System;

namespace ControlSystem.Application.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public TransactionType TransactionType { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}