using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class EmployeeSendDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }

        public DateTime? DateAdded { get; set; }
        public DateTime? DateChangeInfo { get; set; }
        public DateTime? DateOfHiring { get; set; }

    }
}
