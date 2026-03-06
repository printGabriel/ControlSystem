using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ControlSystem.Domain.Projections;

//Como o domain não tem dependência da application, ele não pode utilizar DTO's, então optei por uma
//classe de leitura/projeção.
public class UserFinancialSummary
{
    //id do usuário
    public int UserId { get; set; }
    // nome do usuário
    public string? UserName { get; set; }

    // soma total das receitas do usuário
    public decimal TotalIncome { get; set; }

    // soma total das despesas do usuário
    public decimal TotalExpense { get; set; }

    // saldo do usuário
    public decimal Balance { get; set; }
}