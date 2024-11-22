using ems.application.DTOs.UserLogin;
using ems.application.Interfaces;
using MediatR;
using ems.application.Interfaces.IRepositories;
using AutoMapper;
using ems.application.Interfaces.ITokenService;
using ems.application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ems.application.Constants;
using ems.application.DTOs.BasicAndRoleBaseMenuDTO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ems.application.DTOs.RoleDTO;
using ems.domain.Entity.Masters.RoleInfo;
using ems.application.DTOs.EmployeeDTO;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;

namespace ems.application.Features.UserLoginAndDashboardCmd.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<LoginResponseDTO>>
    {
         
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IUserLoginReopsitory userLoginRepository, IMapper mapper, IUnitOfWork unitOfWork, ITokenService tokenService)
        {
 
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<LoginResponseDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Step 1: Map LoginCommand to LoginRequestDTO
            var loginRequest = new LoginRequestDTO
            {
                LoginId = request.RequestLoginDTO.LoginId,
                Password = request.RequestLoginDTO.Password
            };

            // Step 2: Authenticate User
            var user = await _unitOfWork.UserLoginReopsitory.AuthenticateUser(loginRequest);
            if (user == null || !user.Success)
            {
                return new ApiResponse<LoginResponseDTO>(null, ConstantValues.invalidCredential, ConstantValues.fail);
            }
            // Step 3: Fetch User Roles and Employee Details
            //  var userRoll = await _unitOfWork.UserRoleRepository.GetUsersRolesWithDetailsByIdAsync(user.Id);

            var employee = await _unitOfWork.Employees.GetEmployeeByIdAsync(user.Id);
            // var employeeType = await _unitOfWork.EmployeeTypeRepository.GetEmployeeTypeByIdAsync(employee.EmployeeTypeId);
            // var employeeInfo = _mapper.Map<LoginEmployeeInfoDTO>(employee);
            // Map Employee to LoginEmployeeInfoDTO with LoginId
            var employeeInfo = _mapper.Map<LoginEmployeeInfoDTO>(employee, opt =>
            {
                opt.Items["LoginId"] = loginRequest.LoginId; // Pass LoginId to the context
            });



            // var mpr = _mapper.Map<LoginResponseDTO>(employee);
            // Step 4: Generate JWT Token
            string token = GenerateJwtToken(loginRequest);
            string refreshToken = GenerateRfeshToken(loginRequest);

            // Step 5: Fetch Common Menus
            //   var basicMenus = await _unitOfWork.CommonMenuRepository.GetBasicMenusByUserAndDeviceAsync(1, 1);

            var expireTime = DateTime.Now.AddMinutes(36);
            // Step 6: Prepare Response DTO
            // Step 1: Prepare Login Response DTO
            var loginResponse = new LoginResponseDTO
            {
                Token = token,
                ExpireWithin = expireTime.ToString("yyyy-MM-dd HH:mm:ss"), // Corrected format
                RefreshToken = refreshToken,
                Success = ConstantValues.isSucceeded,  // Make sure success is set to true if login is successful
                EmployeeInfo = employeeInfo // Bind EmployeeInfo
              
            };

            // Step 2: Return Response wrapped in ApiResponse
            return new ApiResponse<LoginResponseDTO>(loginResponse, ConstantValues.successMessage, ConstantValues.isSucceeded);
            
        }

        private string GenerateRfeshToken(LoginRequestDTO loginRequest)
        {
            return "w4lfsd #@@DSsdF@#fsdgfsgfsdklfdsdflksdfl34f$#%#4345463#FEF^$^&#%#Rergfgfsfgfgsfh46trtpoj4opyu40[gshegne;k q3";
        }

        private string GenerateJwtToken(LoginRequestDTO user)
        {
            return _tokenService.GenerateToken(user);
        }
    }
}
