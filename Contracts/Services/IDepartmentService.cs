using Entities.DTO.Create;
using Entities.DTO.Send;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IDepartmentService : IService<DepartmentSendDTO,DepartmentCreateUpdateDTO>
    {
        Task<IEnumerable<DepartmentSendDTO>> GetAllPagging(DepartmentParameters departmentParameters, HttpResponse response);
    }
}
