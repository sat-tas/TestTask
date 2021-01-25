using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repository
{
    public interface IPositionRepository
    {
        Task<IEnumerable<Position>> GetPositionsAsync(bool trackChanges);
        Task<Position> GetPositionAsync(int positionId, bool trackChanges);
        void CreatePosition(Position position);
        void DeletePosition(Position position);
    }

}
