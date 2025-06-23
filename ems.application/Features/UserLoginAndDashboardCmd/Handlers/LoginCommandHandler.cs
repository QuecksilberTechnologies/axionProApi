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
using ems.application.DTOs.Module;

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
                    Longitude = request.RequestLoginDTO.Longitude
                };

                // 🔐 Step 1: Validate if user exists
                 long empId = await _unitOfWork.CommonRepository.ValidateActiveUserLoginOnlyAsync(loginRequest.LoginId);
                _logger.LogInformation("Validation result for LoginId {LoginId}: EmployeeId = {empId}", loginRequest.LoginId, empId);

                if (empId < 1)
                {
                    _logger.LogWarning("User validation failed for LoginId: {LoginId}", loginRequest.LoginId);
                    await _unitOfWork.RollbackTransactionAsync();
                    return ApiResponse<LoginResponseDTO>.Fail("User is not authenticated or authorized to perform this action.");
                }

                // 🔐 Step 2: Authenticate credentials
                var user = await _unitOfWork.UserLoginRepository.AuthenticateUser(loginRequest);
                if (user == null)
                {
                    return ApiResponse<LoginResponseDTO>.Fail(ConstantValues.invalidCredential);
                }

                // 🔐 Step 3: Generate tokens
                var token = await _tokenService.GenerateToken(loginRequest.LoginId.ToString());
                var refreshToken = await _tokenService.GenerateRefreshToken();

                await _refreshTokenRepository.SaveOrUpdateRefreshToken(
                    loginRequest.LoginId.ToString(),
                    token,
                    ConstantValues.ExpireTokenDate,
                    ConstantValues.IP
                );

                // 🔄 Step 4: Update login audit
                bool updated = await _unitOfWork.CommonRepository.UpdateLoginCredential(loginRequest);
                if (updated)
                    _logger.LogInformation("LoginCredential updated successfully for LoginId: {LoginId}", loginRequest.LoginId);
                else
                    _logger.LogWarning("Failed to update LoginCredential for LoginId: {LoginId}", loginRequest.LoginId);

                // 👨‍💼 Step 5: Fetch Employee Info
                var employee = await _unitOfWork.Employees.GetEmployeeInfoForLoginByIdAsync(empId);
                if (employee==null)
                {
                    _logger.LogWarning("Employee may not active or deleted please  contact admin  {LoginId}", loginRequest.LoginId);

                }

                // 👥 Step 6: Fetch all roles
                var userRoles = await _unitOfWork.UserRoleRepository.GetEmployeeRolesWithDetailsByIdAsync(empId);
                string? allRoleIdsCsv = userRoles?
                    .Where(r => r.RoleId != null)
                    .Select(r => r.RoleId.ToString())
                    .Aggregate((a, b) => $"{a},{b}");

                if (!string.IsNullOrEmpty(allRoleIdsCsv))
                    _logger.LogInformation("Fetched Role IDs for LoginId {LoginId}: {Roles}", loginRequest.LoginId, allRoleIdsCsv);
                else
                    _logger.LogWarning("No roles found for LoginId {LoginId}", loginRequest.LoginId);


                // 🧠 Step 7: Map roles to DTOs
                List<UserRoleDTO> userRoleDTOs = _mapper.Map<List<UserRoleDTO>>(userRoles);

                // ✅ Find & separate primary role
                var primaryRole = userRoleDTOs.FirstOrDefault(ur => ur.IsPrimaryRole && ur.IsActive);
                if (primaryRole != null)
                    userRoleDTOs.Remove(primaryRole); // Remove primary from list

                // 🎯 Extract secondary role IDs
                List<int> secondaryRoleIds = userRoleDTOs
                    .Where(ur => ur.RoleId > 0)
                    .Select(ur => ur.RoleId)
                    .ToList();

                string secondaryRolesCsv = secondaryRoleIds.Any()
                    ? string.Join(",", secondaryRoleIds)
                    : "NULL";

                // 🧾 Step 8: Map Employee Info
                var employeeInfo = _mapper.Map<EmployeeLoginInfoDTO>(employee);
                employeeInfo.UserPrimaryRole = primaryRole;
                employeeInfo.UserSecondryRoles = userRoleDTOs;

                // 🧩 Step 9: Load Common Items
                //  var commonItems = await _unitOfWork.CommonRepository.GetCommonItemAsync();

                var parent = await _unitOfWork.ModuleRepository.GetCommonMenuParentAsync();
                if (parent == null) return null;

               List < Module > children = await _unitOfWork.ModuleRepository.GetCommonMenuTreeAsync(parent.Id);

                List<CommonItemDTO> childDTOs = _mapper.Map<List<CommonItemDTO>>(children);
                
                // 🔐 Step 10: Fetch Permissions
                bool isActive = true, hasAccess = true;
                var rolePermissions = await _unitOfWork.CommonRepository.GetModulePermissionsAsync(empId, allRoleIdsCsv, hasAccess, isActive);

                var permissionList = new List<List<RoleModulePermission>> { rolePermissions };

                // 🚀 Step 11: Final Response Object
                var loginResponse = new LoginResponseDTO
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Success = ConstantValues.isSucceeded,
                    EmployeeInfo = employeeInfo,
                    CommonItems = childDTOs,
                    OperationalMenus = permissionList,
                    Allroles = allRoleIdsCsv?.Trim()
                };

                // ✅ Final Return
                return ApiResponse<LoginResponseDTO>.Success(loginResponse, "Login successful.");

                

              //  return new ApiResponse<LoginResponseDTO>(loginResponse, ConstantValues.successMessage, ConstantValues.isSucceeded);
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
