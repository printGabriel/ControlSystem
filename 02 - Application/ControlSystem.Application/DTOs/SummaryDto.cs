using System;
using System.Collections.Generic;
using System.Text;

namespace ControlSystem.Application.DTOs
{
    public class SummaryDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance { get; set; }
    }
}
