using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ControlSystem.Domain.Projections;

//Como o domain não tem dependência da application, ele não pode utilizar DTO's, então optei por uma
//classe de leitura.
public class UserFinancialSummary
{
    public int UserId { get; set; }
    public string? UserName { get; set; }

    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }

    public decimal Balance { get; set; }
}