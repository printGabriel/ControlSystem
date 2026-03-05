using ControlSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

public class TransactionProjection
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public int TransactionType { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
}
