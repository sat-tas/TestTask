using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Position
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
