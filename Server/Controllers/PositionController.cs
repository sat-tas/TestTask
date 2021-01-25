using AutoMapper;
using Contracts;
using Contracts.Services;
using Entities;
using Entities.DTO.Create;
using Entities.DTO.Send;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Server.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/position/[action]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IPositionService _positionService;
        public PositionController(ILoggerManager logger, IMapper mapper, IPositionService service)
        {
            _logger = logger;
            _mapper = mapper;
            _positionService = service;
        }

        [HttpGet(Name = "GetAllPositions")]
        public async Task<IActionResult> GetAllPositions()
        {
            var positionsDTO = await _positionService.GetAll();
            return Ok(positionsDTO);
        }

        [HttpGet("{id}", Name = "GetPosition")]
        public async Task<IActionResult> GetPosition(int id)
        {
            var positionDTO = await _positionService.GetById(id);
            return Ok(positionDTO);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePosition([FromBody] PositionCreateUpdateDTO position)
        {
            var positionDTO = await _positionService.Create(position);
            return CreatedAtRoute("GetPosition", new
            {
                id = positionDTO.Id
            },
            positionDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            await _positionService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdatePosition(int id, [FromBody] PositionCreateUpdateDTO positionDTO)
        {
            await _positionService.Update(id, positionDTO);
            return NoContent();
        }

    }
}
