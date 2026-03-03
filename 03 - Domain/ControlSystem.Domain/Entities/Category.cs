using System;

namespace ControlSystem.Domain.Entities
{
    public class Category
    {
        public int Id { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public int PurposeType { get; private set; }
        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

        public Category(string description, int purposeType)
        {
            Description = description;
            PurposeType = purposeType;
        }

        public void Update(string description, int purposeType)
        {
            Description = description;
            PurposeType = purposeType;
        }
    }
}