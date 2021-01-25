using Entities.DTO.Create;
using Entities.DTO.Send;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IEmployeeService : IService<EmployeeSendDTO,EmployeeCreateUpdateDTO>
    {
        public Task<IEnumerable<EmployeeSendDTO>> GetAll(EmployeeParameters emloyeeParameters, HttpResponse response);
        public Task<IEnumerable<EmployeeSendDTO>> GetByDepartment(int departmentId,EmployeeParameters emloyeeParameters, HttpResponse response);
        public Task<EmployeeSendDTO> PartiallyUpdate(int id, JsonPatchDocument<EmployeeCreateUpdateDTO> patchDoc);

    }
}
