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
    [Route("api/employee/[action]")]
    [ApiController]
    public class EmplyeeController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        public EmplyeeController(ILoggerManager logger, IMapper mapper, IEmployeeService service)
        {
            _logger = logger;
            _mapper = mapper;
            _employeeService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAll();
            return Ok(employees);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesByDepartment(int departmentId, [FromQuery] EmployeeParameters employeeParameters)
        {
            var employees = await _employeeService.GetByDepartment(departmentId, employeeParameters,Response);
            return Ok(employees);
        }
        

        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeParameters employeeParameters)
        {
            var employees = await _employeeService.GetAll(employeeParameters,Response);
            return Ok(employees);
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetById(id);
            return Ok(employee);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateUpdateDTO employee)
        {
            var employeeDTO = await _employeeService.Create(employee);
            return Ok(employeeDTO.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeCreateUpdateDTO employeeDTO)
        {
            await _employeeService.Update(id, employeeDTO);
            return NoContent();
        }

        [HttpPatch("{employeeId}")]
        public async Task<IActionResult> PartiallyUpdateTask(int employeeId, [FromBody] JsonPatchDocument<EmployeeCreateUpdateDTO> patchDoc)
        {
            var employeeDTO=await _employeeService.PartiallyUpdate(employeeId, patchDoc);
            return Ok(employeeDTO.Id);
        
        }


    }
}
