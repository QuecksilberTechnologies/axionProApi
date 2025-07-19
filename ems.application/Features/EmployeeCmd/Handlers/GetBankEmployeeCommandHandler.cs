using AutoMapper;
using ems.application.Common.Commands;
using ems.application.DTOs.Employee;
using ems.application.DTOs.Employee.AccessResponse;
using ems.application.Features.EmployeeCmd.Commands;
using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
using ems.application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ems.application.Features.EmployeeCmd.Handlers
{
    public class GetBankEmployeeCommandHandler : IRequestHandler<GetEmployeeBankDetailCommand, ApiResponse<GetEmployeeBankResponseDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetBankEmployeeCommandHandler> _logger;

        public GetBankEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork,
            ILogger<GetBankEmployeeCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<GetEmployeeBankResponseDTO>> Handle(GetEmployeeBankDetailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return ApiResponse<GetEmployeeBankResponseDTO>.Fail("Unauthorized: User ID not found in token.");

                var loginId = userIdClaim.Value;
                long empId = await _unitOfWork.CommonRepository.ValidateActiveUserLoginOnlyAsync(loginId);
                if (empId < 1)
                    return ApiResponse<GetEmployeeBankResponseDTO>.Fail("Employee not found for current user.");

                // ✅ Validate input DTO
                if (request.DTO == null || request.DTO.EmployeeId <= 0)
                    return ApiResponse<GetEmployeeBankResponseDTO>.Fail("Invalid request payload.");

                // ✅ Fetch employee bank info
                var employeeEntity = await _employeeRepository.GetEmployeeBankInfoByIdAsync(request.DTO.EmployeeId);
                if (employeeEntity == null)
                    return ApiResponse<GetEmployeeBankResponseDTO>.Fail("Employee bank details not found.");

                // ✅ Map and return response
                var mappedDto = _mapper.Map<GetEmployeeBankResponseDTO>(employeeEntity);
                return ApiResponse<GetEmployeeBankResponseDTO>.Success(mappedDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employee bank info for EmployeeId: {EmployeeId}", request.DTO?.EmployeeId);
                return ApiResponse<GetEmployeeBankResponseDTO>.Fail("Something went wrong", new List<string> { ex.Message });
            }
        }


    }

}
