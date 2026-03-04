using System;
using System.Collections.Generic;
using System.Text;

namespace ControlSystem.Application.DTOs
{
    public class FinancialSummaryResponse
    {
        public List<SummaryDto> Users { get; set; } = new();

        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
