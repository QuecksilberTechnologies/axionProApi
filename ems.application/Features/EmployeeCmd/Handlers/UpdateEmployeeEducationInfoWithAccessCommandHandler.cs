using AutoMapper;
using ems.application.Common.Helpers;
using ems.application.DTOs.Employee.AccessControlType;
using ems.application.DTOs.Employee.AccessResponse;
using ems.application.Features.EmployeeCmd.Commands;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.EmployeeCmd.Handlers
{
    public class UpdateEmployeeEducationInfoWithAccessCommandHandler : IRequestHandler<UpdateEmployeeEducationInfoWithAccessCommand, ApiResponse<bool>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateEmployeeEducationInfoWithAccessCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateEmployeeEducationInfoWithAccessCommandHandler(
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork,
            ILogger<UpdateEmployeeEducationInfoWithAccessCommandHandler> logger,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Handle(UpdateEmployeeEducationInfoWithAccessCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.updateEmployeeInfoWithAccessRequest;

                if (string.IsNullOrWhiteSpace(dto.FieldName))
                    return ApiResponse<bool>.Fail("Field name cannot be empty.");

                EmployeeEducation? employeeEducationInfo = await _employeeRepository.GetEmployeeEducationByIdAsync(dto.EmployeeId);
                if (employeeEducationInfo == null)
                    return ApiResponse<bool>.Fail("Employee record not found.");
               
                var createDto = _mapper.Map<EmployeeEducationEditableFieldsDTO>(employeeEducationInfo);
                var accessDto = EmployeeeEducationInfoMapperHelper.ConvertToAccessResponseDTO(createDto);

                var accessProp = typeof(GetEmployeeEducationInfoWithAccessResponseDTO)
                                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                    .FirstOrDefault(p => string.Equals(p.Name, dto.FieldName, StringComparison.OrdinalIgnoreCase));

                if (accessProp == null)
                    return ApiResponse<bool>.Fail($"Field '{dto.FieldName}' does not exist in the model.");

                var fieldWithAccess = accessProp.GetValue(accessDto);
                var isReadOnlyProp = fieldWithAccess?.GetType().GetProperty("IsReadOnly");
                bool isReadOnly = (bool?)isReadOnlyProp?.GetValue(fieldWithAccess) ?? false;

                if (isReadOnly)
                    return ApiResponse<bool>.Fail($"Field '{dto.FieldName}' is read-only and cannot be modified.");

                var employeeProp = typeof(EmployeeEducation)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(p => string.Equals(p.Name, dto.FieldName, StringComparison.OrdinalIgnoreCase));

                if (employeeProp == null || !employeeProp.CanWrite)
                    return ApiResponse<bool>.Fail($"Field '{dto.FieldName}' is not valid or cannot be written to.");

                if (!TryConvertObjectToValue.TryConvertValue(dto.FieldValue, employeeProp.PropertyType, out object? convertedValue))
                {
                    _logger.LogWarning("Failed to convert value '{Value}' for field '{Field}'", dto.FieldValue, dto.FieldName);
                    return ApiResponse<bool>.Fail($"Unable to convert value for field '{dto.FieldName}'. Please ensure the value is in correct format.");
                }

                employeeProp.SetValue(employeeEducationInfo, convertedValue);  
                employeeEducationInfo.UpdatedById = dto.UpdatedById;
                employeeEducationInfo.UpdatedDateTime = DateTime.UtcNow;

                var updateStatus = await _unitOfWork.Employees.UpdateEmployeeFieldAsync(dto.EmployeeId, dto.EntityName, dto.FieldName, convertedValue, dto.UpdatedById);

                if (!updateStatus)
                    return ApiResponse<bool>.Fail("Unable to update the record. Please try again later.");

                return ApiResponse<bool>.Success(true, $"Field '{dto.FieldName}' updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating the employee education field.");
                return ApiResponse<bool>.Fail("Unexpected error occurred while processing your request.", new List<string> { ex.Message });
            }
        }
    }

}

