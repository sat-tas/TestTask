using AutoMapper;
using Contracts;
using Contracts.Services;
using Entities.DTO.Create;
using Entities.DTO.Send;
using Entities.ErrorModel;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PositionService : IPositionService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public PositionService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PositionSendDTO>> GetAll()
        {
            var positions = await _repository.Position.GetPositionsAsync(false);
            return _mapper.Map<IEnumerable<PositionSendDTO>>(positions);

        }

        public async Task<PositionSendDTO> GetById(int positionId)
        {
            if (positionId > 0)
            {
                var position=await _repository.Position.GetPositionAsync(positionId, false);
                return _mapper.Map<PositionSendDTO>(position);
            }
            throw new CustomError(404, $"Position with id: {positionId} doesn't exist in the database.");
        }

        public async Task<PositionSendDTO> Create(PositionCreateUpdateDTO position)
        {
            var positionEntity = _mapper.Map<Position>(position);
            _repository.Position.CreatePosition(positionEntity);
            await _repository.SaveAsync();
            return _mapper.Map<PositionSendDTO>(positionEntity);
        }

        public async Task Delete(int positionId)
        {
            var postion = await _repository.Position.GetPositionAsync(positionId,true);
            if (postion == null)
            {
                throw new CustomError(404, $"Position with id: {positionId} doesn't exist in the database.");
            }
            _repository.Position.DeletePosition(postion);
            await _repository.SaveAsync();
            return;
        }

        public async Task Update(int positionId, PositionCreateUpdateDTO newPosition)
        {
            var postion = await _repository.Position.GetPositionAsync(positionId, true);
            if (postion == null)
            {
                throw new CustomError(404, $"Position with id: {positionId} doesn't exist in the database.");
            }
            _mapper.Map(newPosition,postion);
            await _repository.SaveAsync();
            return;
        }

    }
}
