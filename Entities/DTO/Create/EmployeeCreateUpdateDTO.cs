using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class EmployeeCreateUpdateDTO
    {
        [Required(ErrorMessage = "Name is a required field.")]
        [MinLength(1, ErrorMessage = "Minimum length for the Name is 1 character")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is a required field.")]
        [MinLength(1, ErrorMessage = "Minimum length for the Surname is 1 character")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Patronymic is a required field.")]
        [MinLength(1, ErrorMessage = "Minimum length for the Patronymic is 1 character")]
        public string Patronymic { get; set; }

        public int PositionId { get; set; }

        public int DepartmentId { get; set; }
    }
}
