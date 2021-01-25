using Contracts.Repository;
using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;

        private PositionRepository _position;
        private DepartmentRepository _department;
        private EmployeeRepository _employee;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IPositionRepository Position
        {
            get
            {
                if (_position == null)
                {
                    _position = new PositionRepository(_repositoryContext);
                }
                return _position;
            }
        }

        public IDepartmentRepository Department
        {
            get
            {
                if (_department == null)
                {
                    _department = new DepartmentRepository(_repositoryContext);
                }
                return _department;
            }
        }

      
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employee == null)
                {
                    _employee = new EmployeeRepository(_repositoryContext);
                }
                return _employee;
            }

        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();

    }
}
