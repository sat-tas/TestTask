using AutoMapper;
using Contracts;
using Contracts.Services;
using Entities.DTO.Create;
using Entities.DTO.Send;
using Entities.ErrorModel;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeSendDTO>> GetAll(EmployeeParameters employeeParameters, HttpResponse response)
        {
            var employees = await _repository.Employee.GetEmployeesAsync(employeeParameters, false);
            response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(employees.MetaData));
            var employeesDTO = _mapper.Map<IEnumerable<EmployeeSendDTO>>(employees);
            return employeesDTO;
        }

        public async Task<IEnumerable<EmployeeSendDTO>> GetAll()
        {
            var employees = await _repository.Employee.GetEmployeesAsync(false);
            var employeesDTO = _mapper.Map<IEnumerable<EmployeeSendDTO>>(employees);
            return employeesDTO;
        }

        public async Task<EmployeeSendDTO> GetById(int employeeId)
        {
            if (employeeId > 0)
            {
                var employee = await _repository.Employee.GetEmployeeAsync(employeeId, false);
                var employeeDTO = _mapper.Map<EmployeeSendDTO>(employee);
                return employeeDTO;
            }
            throw new CustomError(404, $"Employee with id: {employeeId} doesn't exist in the database.");

        }

        public async Task<EmployeeSendDTO> Create(EmployeeCreateUpdateDTO employee)
        {
            var employeeEntity = _mapper.Map<Employee>(employee);
            _repository.Employee.CreateEmployee(employeeEntity);
            await _repository.SaveAsync();
            return _mapper.Map<EmployeeSendDTO>(employeeEntity);
        }

        public async Task Delete(int employeeId)
        {
            var employee = await _repository.Employee.GetEmployeeAsync(employeeId, true);
            if (employee == null)
            {
                throw new CustomError(404, $"Employee with id: {employeeId} doesn't exist in the database.");
            }
            _repository.Employee.DeleteEmployee(employee);
            await _repository.SaveAsync();
            return;
        }

        public async Task Update(int employeeId, EmployeeCreateUpdateDTO newEmployee)
        {
            var employee = await _repository.Employee.GetEmployeeAsync(employeeId, true);
            if (employee == null)
            {
                throw new CustomError(404, $"Employee with id: {employeeId} doesn't exist in the database.");
            }
            _mapper.Map(newEmployee, employee);
            await _repository.SaveAsync();
            return;
        }

        public async Task<IEnumerable<EmployeeSendDTO>> GetByDepartment(int departmentId, EmployeeParameters employeeParameters, HttpResponse response)
        {
            var employees = await _repository.Employee.GetEmployeesbyDepartmentAsync(departmentId, employeeParameters, false);
            response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(employees.MetaData));
            var employeesDTO = _mapper.Map<IEnumerable<EmployeeSendDTO>>(employees);
            return employeesDTO;
        }

        public async Task<EmployeeSendDTO> PartiallyUpdate(int id, JsonPatchDocument<EmployeeCreateUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                throw new CustomError(404, $"patchDoc object sent from client is null.");
            }

            var employeeEntity = await _repository.Employee.GetEmployeeAsync(id,true);
            if (employeeEntity == null)
            {
                throw new CustomError(404, $"Employee with id: {id} doesn't exist in the database.");
            }

            var employeeToPatch = _mapper.Map<EmployeeCreateUpdateDTO>(employeeEntity);
            patchDoc.ApplyTo(employeeToPatch);
            _mapper.Map(employeeToPatch, employeeEntity);
            if (employeeEntity.DepartmentId==null && employeeEntity.PositionId!=null)
            {
                throw new CustomError(404, $"Department must be selected");
            }
            await _repository.SaveAsync();
            return _mapper.Map<EmployeeSendDTO>(employeeEntity);
        }
    }
}
