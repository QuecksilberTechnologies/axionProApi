using AutoMapper;
using ems.application.DTOs.Employee;
using ems.application.Features.EmployeeCmd.Commands;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ems.application.Features.EmployeeCmd.Handlers
{
    public class GetEmployeePersonalDetailCommandHandler : IRequestHandler<GetEmployeePersonalDetailCommand, ApiResponse<GetEmployeePersonalDetailResponseDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetEmployeePersonalDetailCommandHandler> _logger;

        public GetEmployeePersonalDetailCommandHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork,
            ILogger<GetEmployeePersonalDetailCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<GetEmployeePersonalDetailResponseDTO>> Handle(GetEmployeePersonalDetailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return ApiResponse<GetEmployeePersonalDetailResponseDTO>.Fail("Unauthorized: User ID not found in token.");

                var loginId = userIdClaim.Value;
                long empId = await _unitOfWork.CommonRepository.ValidateActiveUserLoginOnlyAsync(loginId);
                if (empId < 1)
                    return ApiResponse<GetEmployeePersonalDetailResponseDTO>.Fail("Employee not found for current user.");

                if (request.DTO == null || request.DTO.EmployeeId <= 0)
                    return ApiResponse<GetEmployeePersonalDetailResponseDTO>.Fail("Invalid request payload.");

                var employeePersonalInfo = await _employeeRepository.GetEmployeePersonalInfoByIdAsync(request.DTO.EmployeeId);
                if (employeePersonalInfo == null)
                    return ApiResponse<GetEmployeePersonalDetailResponseDTO>.Fail("Employee personal info not found.");

                var mappedDto = _mapper.Map<GetEmployeePersonalDetailResponseDTO>(employeePersonalInfo);
                return ApiResponse<GetEmployeePersonalDetailResponseDTO>.Success(mappedDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employee personal info for EmployeeId: {EmployeeId}", request.DTO?.EmployeeId);
                return ApiResponse<GetEmployeePersonalDetailResponseDTO>.Fail("Something went wrong", new List<string> { ex.Message });
            }
        }
    }
}
