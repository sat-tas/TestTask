using Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IPositionRepository Position { get; }
        IDepartmentRepository Department { get; }
        IEmployeeRepository Employee { get; }
        Task SaveAsync();
    }
}
