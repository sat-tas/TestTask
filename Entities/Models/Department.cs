using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        [MinLength(1, ErrorMessage = "Minimum length for the Name is 1 character")]
        public String Name { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateChangeInfo { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
