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
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        
        public async Task<EmployeeDTO> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
           try
            {
                // DTO to Entity Mapping - Ensure to map from EmployeeCreateDto to Employee
              //  var employeeEntity = _mapper.Map<Employee>(request.EmployeeDTO);

                var employee = new Employee
                {
                    
                    EmployeeDocumentId = 11,
                    EmployementCode = "EMP25",
                    LastName = "yadav",
                    MiddleName = "A",
                    FirstName = "ranjeet",
                    DateOfBirth = new DateOnly(1990, 5, 15),
                    DateOfOnBoarding = new DateOnly(2023, 8, 1),
                    DateOfExit = null,  // Employee is still active
                    SpecializationId = 12,
                    DesignationId = 3,
                    EmployeeTypeId = 1,
                    DepartmentId = 5,
                    OfficialEmail = "ranjeet.doe@example.com",
                    HasPermanent = true,
                    IsActive = true,
                    FunctionalId = 7,
                    ReferalCode = "REF12345",
                    AddedById = 1001,
                    AddedDateTime = DateTime.Now,
                    UpdatedById = 1001,
                    UpdatedDateTime = DateTime.Now
                };


                // Database insert
                var createdEmployee = await _employeeRepository.AddAsync(employee);
            await _unitOfWork.CommitAsync();
            // Entity to DTO Mapping for Response - Ensure this maps Employee to EmployeeDTO
            return _mapper.Map<EmployeeDTO>(createdEmployee);

            
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

    }




}
