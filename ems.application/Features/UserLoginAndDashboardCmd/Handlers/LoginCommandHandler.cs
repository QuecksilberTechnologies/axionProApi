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
 
using static System.Runtime.InteropServices.JavaScript.JSType;
 
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
 
using ems.application.DTOs.Employee;
using Microsoft.Extensions.Logging;
using ems.application.Features.AssetCmd.Handlers;
using ems.domain.Entity;
using ems.application.DTOs.UserRole;
using Microsoft.VisualBasic;

namespace ems.application.Features.UserLoginAndDashboardCmd.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<LoginResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewTokenRepository _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, INewTokenRepository tokenService, IRefreshTokenRepository refreshTokenRepository, ILogger<LoginCommandHandler> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<ApiResponse<LoginResponseDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loginRequest = new LoginRequestDTO
                {
                    LoginId = request.RequestLoginDTO.LoginId,
                    Password = request.RequestLoginDTO.Password,
                    IpAddressLocal = request.RequestLoginDTO.IpAddressLocal,
                    IpAddressPublic = request.RequestLoginDTO.IpAddressPublic,
                    LoginDevice = request.RequestLoginDTO.LoginDevice,
                    MacAddress = request.RequestLoginDTO.MacAddress,
                    Latitude = request.RequestLoginDTO.Latitude,
                    Longitude = request.RequestLoginDTO.Longitude,
                };

                var existUserWithEmpId = await _unitOfWork.CommonRepository.ValidateUserLoginAsync(loginRequest.LoginId);
                _logger.LogInformation("Validation result: {existUserWithEmpId}", existUserWithEmpId);

                if (existUserWithEmpId < 1)
                {
                    _logger.LogWarning("User validation failed. Rolling back transaction.");
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ApiResponse<LoginResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "User is not authenticated or authorized to perform this action.",
                        Data = null
                    };
                }

                // ✅ Generate JWT Token & Refresh Token
                var user = await _unitOfWork.UserLoginRepository.AuthenticateUser(loginRequest);
                if (user == null)
                {
                    return new ApiResponse<LoginResponseDTO>(null, ConstantValues.invalidCredential, ConstantValues.fail);
                }

                string token = await _tokenService.GenerateToken(loginRequest.LoginId.ToString());
                string refreshToken = await _tokenService.GenerateRefreshToken();
                var isSaved = await _refreshTokenRepository.SaveOrUpdateRefreshToken(
                    loginRequest.LoginId.ToString(),
                    token,
                    ConstantValues.ExpireTokenDate,
                    ConstantValues.IP
                );

                var updatedLoginMessage = await _unitOfWork.CommonRepository.UpdateLoginCredential(loginRequest);
                _logger.LogInformation("UpdateLoginCredential executed successfully for {LoginId}", loginRequest.LoginId);

                 Employee? employee = await _unitOfWork.Employees.GetEmployeeInfoForLoginByIdAsync(existUserWithEmpId);


                List<UserRole> userRoles = await _unitOfWork.UserRoleRepository
                                                .GetEmployeeRolesWithDetailsByIdAsync(existUserWithEmpId);
                string AllroleIdsCsv = null;
                // Null check to avoid exceptions
                if (userRoles != null && userRoles.Any())
                {
                    // RoleId extract karke comma-separated string banayein
                       AllroleIdsCsv = string.Join(", ", userRoles
                        .Where(ur => ur.RoleId != null)  // NULL RoleId ko hatao
                        .Select(ur => ur.RoleId.ToString()) // Convert to string
                    );

                    // Debug/Check output
                    Console.WriteLine($"RoleIds CSV: {AllroleIdsCsv}");
                }
                else
                {
                    Console.WriteLine("No roles found or userRoles is null.");
                }


                List<UserRoleDTO> userRoleDTOs = _mapper.Map<List<UserRoleDTO>>(userRoles);

                // ✅ Find Primary Role
                UserRoleDTO? EmployeePrimaryUserRole = userRoleDTOs.FirstOrDefault(ur => ur.IsPrimaryRole == true && ur.IsActive == true);
                
                // ✅ Remove Primary Role from List
                if (EmployeePrimaryUserRole != null)
                {
                    userRoleDTOs.Remove(EmployeePrimaryUserRole);
                }
                // ✅ Extract Only RoleId & RoleName
                List<int>? SecondryRoleList = userRoleDTOs.Where(ur => ur.RoleId > 0).Select(ur => ur.RoleId).ToList();
                string roleIdsCsv = (SecondryRoleList != null && SecondryRoleList.Any())
                 ? string.Join(",", SecondryRoleList): "NULL"; // ✅ Null Safe Handling

                // ✅ Map Employee to EmployeeLoginInfoDTO
                EmployeeLoginInfoDTO employeeInfo = _mapper.Map<EmployeeLoginInfoDTO>(employee);

                // ✅ Assign Primary Role & Other Roles
                employeeInfo.UserPrimaryRole = EmployeePrimaryUserRole;
                employeeInfo.UserSecondryRoles = userRoleDTOs;
                 List<CommonItem> tem = await _unitOfWork.CommonRepository.GetCommonItemAsync();
                bool isactive = true;
                bool hasaccess = true;
                List<List<RoleModulePermission>> rolespermission = new List<List<RoleModulePermission>>();

              
                    var permissions = await _unitOfWork.CommonRepository.GetModulePermissionsAsync(existUserWithEmpId, AllroleIdsCsv, hasaccess, isactive);
                    rolespermission.Add(permissions);
               

                await _unitOfWork.CommitTransactionAsync();
                var loginResponse = new LoginResponseDTO
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Success = ConstantValues.isSucceeded,
                    EmployeeInfo = employeeInfo,
                    CommonItems = tem,
                    OperationalMenus = rolespermission,
                    Allroles = AllroleIdsCsv.Trim()

                };

                return new ApiResponse<LoginResponseDTO>(loginResponse, ConstantValues.successMessage, ConstantValues.isSucceeded);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in LoginCommandHandler.Handle method.");

                await _unitOfWork.RollbackTransactionAsync();  // ✅ Rollback Transaction in case of error

                return new ApiResponse<LoginResponseDTO>
                {
                    IsSucceeded = false,
                    Message = "An error occurred while processing the login request. Please try again later.",
                    Data = null
                };
            }
        }



    }

}
