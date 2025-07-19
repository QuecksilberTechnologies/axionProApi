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
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ems.application.Features.EmployeeCmd.Handlers
{
    public class GetEmployeeExperienceCommandHandler : IRequestHandler<GetEmployeeExperienceCommand, ApiResponse<GetEmployeeExperienceResponseDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetEmployeeExperienceCommandHandler> _logger;

        public GetEmployeeExperienceCommandHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork,
            ILogger<GetEmployeeExperienceCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<GetEmployeeExperienceResponseDTO>> Handle(GetEmployeeExperienceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return ApiResponse<GetEmployeeExperienceResponseDTO>.Fail("Unauthorized: User ID not found in token.");

                var loginId = userIdClaim.Value;
                long empId = await _unitOfWork.CommonRepository.ValidateActiveUserLoginOnlyAsync(loginId);
                if (empId < 1)
                    return ApiResponse<GetEmployeeExperienceResponseDTO>.Fail("Employee not found for current user.");

                if (request.DTO == null || request.DTO.EmployeeId <= 0)
                    return ApiResponse<GetEmployeeExperienceResponseDTO>.Fail("Invalid request payload.");

                var employeeExperience = await _employeeRepository.GetEmployeeExperienceInfoByIdAsync(request.DTO.EmployeeId);
                if (employeeExperience == null)
                    return ApiResponse<GetEmployeeExperienceResponseDTO>.Fail("Employee experience not found.");

                var mappedDto = _mapper.Map<GetEmployeeExperienceResponseDTO>(employeeExperience);
                return ApiResponse<GetEmployeeExperienceResponseDTO>.Success(mappedDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employee experience for EmployeeId: {EmployeeId}", request.DTO?.EmployeeId);
                return ApiResponse<GetEmployeeExperienceResponseDTO>.Fail("Something went wrong", new List<string> { ex.Message });
            }
        }
    }
}
