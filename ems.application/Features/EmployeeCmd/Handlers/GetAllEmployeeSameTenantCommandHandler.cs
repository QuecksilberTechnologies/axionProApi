using AutoMapper;
 
using ems.application.DTOs.Employee;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ems.application.Features.EmployeeCmd.Commands;
using ems.domain.Entity;

namespace ems.application.Features.EmployeeCmd.Handlers
{
    public class GetAllEmployeeSameTenantCommandHandler : IRequestHandler<GetAllEmployeeSameTenantCommand, ApiResponse<List<GetEmployeeInfoResponseDTO>>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllEmployeeSameTenantCommandHandler> _logger;

        public GetAllEmployeeSameTenantCommandHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork,
            ILogger<GetAllEmployeeSameTenantCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<List<GetEmployeeInfoResponseDTO>>> Handle(GetAllEmployeeSameTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return ApiResponse<List<GetEmployeeInfoResponseDTO>>.Fail("Unauthorized: User ID not found in token.");

                var loginId = userIdClaim.Value;

                long empId = await _unitOfWork.CommonRepository.ValidateActiveUserLoginOnlyAsync(loginId);
                if (empId < 1)
                    return ApiResponse<List<GetEmployeeInfoResponseDTO>>.Fail("Employee not found for current user.");

                List<Employee> ? employeeLst = await _unitOfWork.Employees.GetAllEmployeesAsync(request.DTO.TenantId);
                if (employeeLst == null)
                    return ApiResponse<List<GetEmployeeInfoResponseDTO>>.Fail("Employee data not found.");

                var mappedDto = _mapper.Map<List<GetEmployeeInfoResponseDTO>>(employeeLst);


                return ApiResponse<List<GetEmployeeInfoResponseDTO>>.Success(mappedDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching employee info.");
                return ApiResponse<List<GetEmployeeInfoResponseDTO>>.Fail("Something went wrong.", new List<string> { ex.Message });
            }
        }
    }



}


