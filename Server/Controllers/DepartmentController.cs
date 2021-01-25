using AutoMapper;
using Contracts;
using Contracts.Services;
using Entities;
using Entities.DTO.Create;
using Entities.DTO.Send;
using Entities.Models;
using Entities.RequestFeatures;
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
    [Route("api/department/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;
        public DepartmentController(ILoggerManager logger, IMapper mapper, IDepartmentService service)
        {
            _logger = logger;
            _mapper = mapper;
            _departmentService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAll();
            return Ok(departments);
        }


        [HttpGet]
        public async Task<IActionResult> GetDepartments([FromQuery] DepartmentParameters departmentParameters)
        {
            var departments = await _departmentService.GetAllPagging(departmentParameters,Response);
            return Ok(departments);
        }

        [HttpGet("{id}", Name = "GetDepartment")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var department = await _departmentService.GetById(id);
            return Ok(department);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentCreateUpdateDTO department)
        {
            var departmentDTO = await _departmentService.Create(department);
            return Ok(departmentDTO.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentCreateUpdateDTO departmentDTO)
        {
            await _departmentService.Update(id, departmentDTO);
            return NoContent();
        }

    }
}
