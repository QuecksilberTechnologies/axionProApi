using System.Reflection;
using ems.application.Common.Helpers;
using ems.application.Features.EmployeeCmd.Commands;
using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using ems.application.DTOs.Employee;

namespace ems.application.Features.EmployeeCmd.Handlers
{
    public class UpdateEmployeeInfoWithAccessCommandHandler : IRequestHandler<UpdateEmployeeInfoWithAccessCommand, ApiResponse<bool>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateEmployeeInfoWithAccessCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateEmployeeInfoWithAccessCommandHandler(
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork,
            ILogger<UpdateEmployeeInfoWithAccessCommandHandler> logger,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Handle(UpdateEmployeeInfoWithAccessCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.updateEmployeeInfoWithAccessRequest;

                if (string.IsNullOrWhiteSpace(dto.FieldName))
                    return ApiResponse<bool>.Fail("Field name is required.");

                var employee = await _employeeRepository.GetEmployeeByIdAsync(dto.EmployeeId);
                if (employee == null)
                    return ApiResponse<bool>.Fail("Employee not found.");

                // 👇 Step 1: Convert to Create DTO
                var createDto = _mapper.Map<GetEditableEmployeeProfileInfoRequestDTO>(employee);

                // 👇 Step 2: Generate FieldWithAccess version (for read-only info)
                var accessDto = EmployeeProfileInfoMapperHelper.ConvertToAccessResponseDTO(createDto);

                // 👇 Step 3: Find property info (case-insensitive)
                var accessProp = typeof(GetEmployeeInfoWithAccessResponseDTO)
                                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                    .FirstOrDefault(p => string.Equals(p.Name, dto.FieldName, StringComparison.OrdinalIgnoreCase));

                if (accessProp == null)
                    return ApiResponse<bool>.Fail($"Field '{dto.FieldName}' does not exist.");

                // 👇 Step 4: Get IsReadOnly value
                var fieldWithAccess = accessProp.GetValue(accessDto);
                var isReadOnlyProp = fieldWithAccess?.GetType().GetProperty("IsReadOnly");
                bool isReadOnly = (bool?)isReadOnlyProp?.GetValue(fieldWithAccess) ?? false;

                if (isReadOnly)
                    return ApiResponse<bool>.Fail($"Field '{dto.FieldName}' is read-only and cannot be updated.");

                // 👇 Step 5: Reflect on actual Employee entity
                var employeeProp = typeof(Employee)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(p => string.Equals(p.Name, dto.FieldName, StringComparison.OrdinalIgnoreCase));

                if (employeeProp == null || !employeeProp.CanWrite)
                    return ApiResponse<bool>.Fail($"Property '{dto.FieldName}' is not valid or not writable.");

                // 👇 Step 6: Type-safe conversion
                if (!TryConvertObjectToValue.TryConvertValue(dto.FieldValue, employeeProp.PropertyType, out object? convertedValue))
                {
                    _logger.LogWarning("Conversion failed for property '{Field}' with value '{Value}'", dto.FieldName, dto.FieldValue);
                    return ApiResponse<bool>.Fail($"Value conversion failed for property '{dto.FieldName}'.");
                }

                // 👇 Step 7: Set value to object
                employeeProp.SetValue(employee, convertedValue);

                // 👇 Step 8: Set audit
                employee.UpdatedById = dto.UpdatedById;
                employee.UpdatedDateTime = DateTime.UtcNow;

                // 👇 Step 9: Save using repository
                var updateStatus = await _unitOfWork.Employees.UpdateEmployeeFieldAsync(dto.EmployeeId, dto.FieldName, convertedValue, dto.UpdatedById);

                if (!updateStatus)
                    return ApiResponse<bool>.Fail("Failed to update employee record.");

                return ApiResponse<bool>.Success(true, $"Field '{dto.FieldName}' updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating employee field.");
                return ApiResponse<bool>.Fail("An unexpected error occurred.", new List<string> { ex.Message });
            }
        }
    }
}
