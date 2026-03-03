using System;
using System.Collections.Generic;
using System.Text;

namespace ControlSystem.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateOnly BirthDate { get; private set; }
    }
}
