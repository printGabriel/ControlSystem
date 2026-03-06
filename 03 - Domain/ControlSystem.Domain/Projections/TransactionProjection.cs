using ControlSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

/*
   Classe de leitura/projeção usada para consultas no banco.

   Como o domínio não deve depender de DTOs da camada de aplicação, criei essa projeção com os campos que eu preciso
*/

public class TransactionProjection
{
    public int Id { get; set; }

    // Descrição da transação
    public string Description { get; set; } = string.Empty;

    // Valor da transação
    public decimal Value { get; set; }

    // Tipo da transação (Receita ou Despesa)
    public int TransactionType { get; set; }

    public int CategoryId { get; set; }

    // Nome da categoria
    public string CategoryName { get; set; }

    public int UserId { get; set; }

    // Nome do usuário responsável
    public string UserName { get; set; }
}
