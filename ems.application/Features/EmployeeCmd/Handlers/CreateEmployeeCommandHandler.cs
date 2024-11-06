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

                Employee employee = new Employee
                {
                 
                    EmployeeDocumentId = 123456,
                    EmployementCode = "EMP001",
                    LastName = "Yadav",
                    MiddleName = "",
                    FirstName = "Yadav",
                    DateOfBirth = new DateOnly(1990, 1, 15), // January 15, 1990
                    DateOfOnBoarding = new DateOnly(2023, 5, 1), // May 1, 2023
                    DateOfExit = null, // Still employed
                    SpecializationId = 2, // Assuming this ID corresponds to a specialization
                    DesignationId = 3, // Assuming this ID corresponds to a designation
                    EmployeeTypeId = 1, // Assuming this ID corresponds to a type of employee
                    DepartmentId = 5, // Assuming this ID corresponds to a department
                    OfficialEmail = "john.doe@example.com",
                    HasPermanent = true,
                    IsActive = true,
                    FunctionalId = 4, // Assuming this ID corresponds to a functional area
                    ReferalCode = "REF123",
                    Remark = "New hire in the IT department.",
                    AddedById = 1001, // ID of the user who added the record
                    AddedDateTime = DateTime.Now, // Current date and time
                    UpdatedById = null, // No updates yet
                    UpdatedDateTime = null // No updates yet
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
