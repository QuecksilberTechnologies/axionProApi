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
    public class GetSelfEmployeementInfoCommandHandler : IRequestHandler<GetSelfEmployeementInfoCommand, ApiResponse<GetEmployeeInfoResponseDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetSelfEmployeementInfoCommandHandler> _logger;

        public GetSelfEmployeementInfoCommandHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork,
            ILogger<GetSelfEmployeementInfoCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<ApiResponse<GetEmployeeInfoResponseDTO>> Handle(GetSelfEmployeementInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return ApiResponse<GetEmployeeInfoResponseDTO>.Fail("Unauthorized: User ID not found in token.");

                var loginId = userIdClaim.Value;
                //      loginId = "embedded.deepesh@gmail.com";

                long empId = await _unitOfWork.CommonRepository.ValidateActiveUserLoginOnlyAsync(loginId);
                if (empId < 1)
                    return ApiResponse<GetEmployeeInfoResponseDTO>.Fail("Employee not found for current user.");

                var employee = await _employeeRepository.GetEmployeeByIdAsync(empId);
                if (employee == null)
                    return ApiResponse<GetEmployeeInfoResponseDTO>.Fail("Employee data not found.");
                GetEmployeeInfoResponseDTO adminDto = _mapper.Map<GetEmployeeInfoResponseDTO>(employee);

                return ApiResponse<GetEmployeeInfoResponseDTO>.Success(adminDto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching self employee info.");
                return ApiResponse<GetEmployeeInfoResponseDTO>.Fail("Something went wrong.", new List<string> { ex.Message });
            }
        }


    }



}


