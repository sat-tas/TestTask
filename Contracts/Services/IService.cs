using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IService<T,V>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int Id);

        Task<T> Create(V value);

        Task Delete(int id);

        Task Update(int id, V value);

    }
}
