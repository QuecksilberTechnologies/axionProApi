using AutoMapper;
using ems.application.DTOs.EmployeeDTO;
using ems.application.Features.EmployeeCmd.Commands;
using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity.EmployeeModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.EmployeeCmd.Handlers
{

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        
        public async Task<EmployeeDTO> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // DTO to Entity Mapping - Ensure to map from EmployeeCreateDto to Employee
            var employeeEntity = _mapper.Map<Employee>(request.EmployeeDTO);

            // Database insert
            var createdEmployee = await _employeeRepository.AddAsync(employeeEntity);

            // Entity to DTO Mapping for Response - Ensure this maps Employee to EmployeeDTO
            return _mapper.Map<EmployeeDTO>(createdEmployee);
        }

    }




}
