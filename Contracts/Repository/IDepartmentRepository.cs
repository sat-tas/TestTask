using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repository
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync(bool trackChanges);
        Task<PagedList<Department>> GetDepartmentsAsync(DepartmentParameters departmentParameters,bool trackChanges);
        Task<Department> GetDepartmentAsync(int departmentId, bool trackChanges);
        void CreateDepartment(Department department);
        void DeleteDepartment(Department department);
    }

}
