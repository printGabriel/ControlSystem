using ControlSystem.Domain.Enums;
using System;

namespace ControlSystem.Domain.Entities
{
    public class Category
    {
        //Id da categoria
        public int Id { get; private set; }

        //descrição da categoria
        public string Description { get; private set; } = string.Empty;

        //tipo da categoria referenciado por um enum
        public PurposeType PurposeType { get; private set; }

        //propriedade de navegação para o relacionamento com Transaction
        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

        //construtor para criar uma nova categoria
        public Category(string description, PurposeType purposeType)
        {
            Description = description;
            PurposeType = purposeType;
        }

        //método para atualizar uma categoria existente
        public void Update(string description, PurposeType purposeType)
        {
            Description = description;
            PurposeType = purposeType;
        }
    }
}