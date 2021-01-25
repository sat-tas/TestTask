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
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.Id).ToListAsync();

        public async Task<PagedList<Department>> GetDepartmentsAsync(DepartmentParameters departmentParameters,bool trackChanges)
        {
            {
                var departments = await FindAll(trackChanges).Search(departmentParameters.SearchTerm).OrderBy(c => c.Id).ToListAsync();
                return PagedList<Department>.ToPagedList(departments, departmentParameters.PageNumber, departmentParameters.PageSize);
            }
        }

        public async Task<Department> GetDepartmentAsync(int positionId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(positionId), trackChanges).SingleOrDefaultAsync();

        public void CreateDepartment(Department position) => Create(position);

        public void DeleteDepartment(Department position) => Delete(position);


    }
}
