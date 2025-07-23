using AutoMapper;
using ems.application.DTOs.Employee;
using ems.application.Features.EmployeeCmd.Commands;
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

namespace ems.application.Features.EmployeeCmd.Handlers
{
    public class GetEmployeeEducationCommandHandler : IRequestHandler<GetEmployeeEducationCommand, ApiResponse<GetEmployeeEducationResponseDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetEmployeeEducationCommandHandler> _logger;

        public GetEmployeeEducationCommandHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork,
            ILogger<GetEmployeeEducationCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<ApiResponse<GetEmployeeEducationResponseDTO>> Handle(GetEmployeeEducationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return ApiResponse<GetEmployeeEducationResponseDTO>.Fail("Unauthorized access: User identity not found.");

                var loginId = userIdClaim.Value;
                long empId = await _unitOfWork.CommonRepository.ValidateActiveUserLoginOnlyAsync(loginId);
                if (empId < 1)
                    return ApiResponse<GetEmployeeEducationResponseDTO>.Fail("No active employee record found for current user.");

                if (request.DTO == null || request.DTO.EmployeeId <= 0)
                    return ApiResponse<GetEmployeeEducationResponseDTO>.Fail("Invalid request: EmployeeId is required.");

                var employeeEducation = await _employeeRepository.GetEmployeeEducationByIdAsync(request.DTO.EmployeeId);
                if (employeeEducation == null)
                    return ApiResponse<GetEmployeeEducationResponseDTO>.Fail("No education details found for the given employee.");

                var responseDto = _mapper.Map<GetEmployeeEducationResponseDTO>(employeeEducation);
                return ApiResponse<GetEmployeeEducationResponseDTO>.Success(responseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching employee education details. EmployeeId: {EmployeeId}", request.DTO?.EmployeeId);
                return ApiResponse<GetEmployeeEducationResponseDTO>.Fail("An unexpected error occurred.", new List<string> { ex.Message });
            }
        }


    }

}