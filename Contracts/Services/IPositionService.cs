using Entities.DTO.Create;
using Entities.DTO.Send;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IPositionService : IService<PositionSendDTO,PositionCreateUpdateDTO>
    {
    }
}
