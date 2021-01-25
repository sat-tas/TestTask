using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTO.Send
{
    public class DepartmentSendDTO
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public DateTime? DateAdded { get; set; }

        public DateTime? DateChangeInfo { get; set; }

    }
}
