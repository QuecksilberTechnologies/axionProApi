using AutoMapper;
using ems.application.Common.Helpers;
using ems.application.Constants;
using ems.application.DTOs.Employee;
using ems.application.Features.EmployeeCmd.Commands;
using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ems.application.Features.EmployeeCmd.Handlers
{
    public class GetEmployeementInfoCommandHandler : IRequestHandler<GetBasicEmployeeInfoCommand, ApiResponse<GetEmployeeInfoResponseDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetEmployeementInfoCommandHandler> _logger;

        public GetEmployeementInfoCommandHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork,
            ILogger<GetEmployeementInfoCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<GetEmployeeInfoResponseDTO>> Handle(GetBasicEmployeeInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return ApiResponse<GetEmployeeInfoResponseDTO>.Fail("Unauthorized: User ID not found in token.");

                var loginId = userIdClaim.Value;

                long empId = await _unitOfWork.CommonRepository.ValidateActiveUserLoginOnlyAsync(loginId);
                if (empId < 1)
                    return ApiResponse<GetEmployeeInfoResponseDTO>.Fail("Employee not found for current user.");

                var employee = await _employeeRepository.GetEmployeeByIdAsync(request.DTO.EmployeeId);
                if (employee == null)
                    return ApiResponse<GetEmployeeInfoResponseDTO>.Fail("Employee data not found.");

                var mappedDto = _mapper.Map<GetEmployeeInfoResponseDTO>(employee);

                return ApiResponse<GetEmployeeInfoResponseDTO>.Success(mappedDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching employee info.");
                return ApiResponse<GetEmployeeInfoResponseDTO>.Fail("Something went wrong.", new List<string> { ex.Message });
            }
        }
    }



}


