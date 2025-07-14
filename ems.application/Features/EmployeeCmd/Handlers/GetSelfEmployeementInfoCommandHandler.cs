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
    public class GetSelfEmployeementInfoCommandHandler : IRequestHandler<GetSelfEmployeementInfoCommand, ApiResponse<GetEmployeeInfoWithAccessResponseDTO>>
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
        public async Task<ApiResponse<GetEmployeeInfoWithAccessResponseDTO>> Handle(GetSelfEmployeementInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim ==null)
                    return ApiResponse<GetEmployeeInfoWithAccessResponseDTO>.Fail("Unauthorized: User ID not found in token.");

                   var loginId = userIdClaim.Value;
            //      loginId = "embedded.deepesh@gmail.com";

                long empId = await _unitOfWork.CommonRepository.ValidateActiveUserLoginOnlyAsync(loginId);
                if (empId < 1)
                    return ApiResponse<GetEmployeeInfoWithAccessResponseDTO>.Fail("Employee not found for current user.");

                var employee = await _employeeRepository.GetEmployeeByIdAsync(empId);
                if (employee == null)
                    return ApiResponse<GetEmployeeInfoWithAccessResponseDTO>.Fail("Employee data not found.");
                if (employee.IsEditAllowed == true || employee.IsEditAllowed == null)
                {

                    GetEditableEmployeeProfileInfoRequestDTO adminDto = _mapper.Map<GetEditableEmployeeProfileInfoRequestDTO>(employee);

                    Console.WriteLine("Verification status not set.");
                    var accessDto = EmployeeProfileInfoMapperHelper.ConvertToAccessResponseDTO(adminDto);

                    return ApiResponse<GetEmployeeInfoWithAccessResponseDTO>.Success(accessDto);
                }
                else
                {
                    GetDisabledEmployeeProfileInfoRequestDTO adminDto = _mapper.Map<GetDisabledEmployeeProfileInfoRequestDTO>(employee);

                    var accessDto = EmployeeProfileInfoMapperHelper.ConvertToAccessResponseDTO(adminDto);
                 

                    return ApiResponse<GetEmployeeInfoWithAccessResponseDTO>.Success(accessDto);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching self employee info.");
                return ApiResponse<GetEmployeeInfoWithAccessResponseDTO>.Fail("Something went wrong.", new List<string> { ex.Message });
            }
        }


    }



}


