using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(bool trackChanges);
        Task<PagedList<Employee>> GetEmployeesAsync(EmployeeParameters emloyeeParameters,bool trackChanges);
        Task<PagedList<Employee>> GetEmployeesbyDepartmentAsync(int idDepartment, EmployeeParameters employeeParameters, bool trackChanges);
        Task<Employee> GetEmployeeAsync(int employeeId, bool trackChanges);
        void CreateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }

}
