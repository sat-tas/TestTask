using Contracts.Repository;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(bool trackChanges) => await FindAll(trackChanges).Include(d=>d.Department).Include(p=>p.Position).OrderBy(c => c.Name).ToListAsync();

        public async Task<PagedList<Employee>> GetEmployeesbyDepartmentAsync(int idDepartment, EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindAll(trackChanges).Where(e=>e.DepartmentId==idDepartment).Search(employeeParameters.SearchTerm).Include(d => d.Department).Include(p => p.Position).OrderBy(c => c.Name).ToListAsync();
            return PagedList<Employee>.ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);
        }

        public async Task<PagedList<Employee>> GetEmployeesAsync(EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindAll(trackChanges).Search(employeeParameters.SearchTerm).Include(d => d.Department).Include(p => p.Position).OrderBy(c => c.Name).ToListAsync();
            return PagedList<Employee>.ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);
        }

        public async Task<Employee> GetEmployeeAsync(int employeeId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(employeeId), trackChanges).Include(d => d.Department).Include(p => p.Position).SingleOrDefaultAsync();

        public void CreateEmployee(Employee employee) => Create(employee);

        public void DeleteEmployee(Employee employee) => Delete(employee);

    }
}
