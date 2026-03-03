using System;
using System.Collections.Generic;
using System.Text;

namespace ControlSystem.Domain.Entities
{
    public class Category
    {        public int Id { get; private set; }
        public string Description { get; private set; }
        public int PurposeType { get; private set; }

        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();
    }
}
