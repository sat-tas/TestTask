using Contracts.Repository;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class PositionRepository : RepositoryBase<Position>, IPositionRepository
    {
        public PositionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Position>> GetPositionsAsync(bool trackChanges)=> await FindAll(trackChanges).OrderBy(p => p.Id).ToListAsync();
        
        public async Task<Position> GetPositionAsync(int positionId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(positionId), trackChanges).SingleOrDefaultAsync();

        public void CreatePosition(Position position) => Create(position);

        public void DeletePosition(Position position) => Delete(position);
    }
}
