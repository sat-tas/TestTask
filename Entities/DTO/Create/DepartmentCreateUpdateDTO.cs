using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    public class DepartmentCreateUpdateDTO
    {
        [Required(ErrorMessage = "Name is a required field.")]
        [MinLength(1, ErrorMessage = "Minimum length for the Name is 1 character")]
        public String Name { get; set; }
    }
}
