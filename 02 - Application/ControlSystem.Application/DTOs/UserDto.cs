using System;
using System.Collections.Generic;
using System.Text;

namespace ControlSystem.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
