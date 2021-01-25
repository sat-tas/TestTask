using AutoMapper;
using Contracts;
using Contracts.Services;
using Entities.DTO.Create;
using Entities.DTO.Send;
using Entities.ErrorModel;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public DepartmentService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentSendDTO>> GetAllPagging(DepartmentParameters departmentParameters,HttpResponse response)
        {
            var departments = await _repository.Department.GetDepartmentsAsync(departmentParameters,false);
            response.Headers.Add("x-pagination", JsonConvert.SerializeObject(departments.MetaData));
            var departmentsDTO = _mapper.Map<IEnumerable<DepartmentSendDTO>>(departments);
            return departmentsDTO;
        }

        public async Task<IEnumerable<DepartmentSendDTO>> GetAll()
        {
            var departments = await _repository.Department.GetDepartmentsAsync(false);
            var departmentsDTO = _mapper.Map<IEnumerable<DepartmentSendDTO>>(departments);
            return departmentsDTO;
        }


        public async Task<DepartmentSendDTO> GetById(int departmentId)
        {
            if (departmentId > 0)
            {
                var department= await _repository.Department.GetDepartmentAsync(departmentId, false);
                var departmentDTO =_mapper.Map<DepartmentSendDTO>(department);
                return departmentDTO;
            }
            throw new CustomError(404, $"Department with id: {departmentId} doesn't exist in the database.");
        }

        public async Task<DepartmentSendDTO> Create(DepartmentCreateUpdateDTO department)
        {
            var departmentEntity = _mapper.Map<Department>(department);
            _repository.Department.CreateDepartment(departmentEntity);
            await _repository.SaveAsync();
            return _mapper.Map<DepartmentSendDTO>(departmentEntity);
        }

        public async Task Delete(int departmentId)
        {
            //default value
            if (departmentId==1)
            {
                throw new CustomError(404, $"Department with id: {departmentId} couldn't be deleted.");
            }
            var department = await _repository.Department.GetDepartmentAsync(departmentId, true);
            if (department == null)
            {
                throw new CustomError(404,$"Department with id: {departmentId} doesn't exist in the database.");
            }
            _repository.Department.DeleteDepartment(department);
            await _repository.SaveAsync();
            return;
        }

        public async Task Update(int departmentId, DepartmentCreateUpdateDTO newDepartment)
        {
            var department = await _repository.Department.GetDepartmentAsync(departmentId, true);
            if (department == null)
            {
                throw new CustomError(404, $"Department with id: {departmentId} doesn't exist in the database.");
            }
            _mapper.Map(newDepartment, department);
            await _repository.SaveAsync();
            return;
        }

    }
}
