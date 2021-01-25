using AutoMapper;
using Entities.DTO.Create;
using Entities.DTO.Send;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Position, PositionCreateUpdateDTO>().ReverseMap();
            CreateMap<Position, PositionSendDTO>();
            CreateMap<Department, DepartmentCreateUpdateDTO>().ReverseMap();
            CreateMap<Department, DepartmentSendDTO>();
            CreateMap<Employee, EmployeeCreateUpdateDTO>().ReverseMap();
            CreateMap<Employee, EmployeeSendDTO>().AfterMap((e,s)=> {
                if (e.PositionId != null && e.Position!=null)
                    s.PositionName = e.Position.Name;
                if (e.DepartmentId != null && e.Department != null)
                    s.DepartmentName = e.Department.Name;
            });
        }

    }
}
