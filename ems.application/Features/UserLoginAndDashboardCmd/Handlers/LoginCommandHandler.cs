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
using System.Reflection;
using ems.application.DTOs.RoleModulePermission;
using ems.application.DTOs.Operation;
using ems.application.DTOs.SubscriptionModule;

namespace ems.application.Features.UserLoginAndDashboardCmd.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<LoginResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewTokenRepository _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ICommonRepository _iCommonRepository;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, INewTokenRepository tokenService, IRefreshTokenRepository refreshTokenRepository, ILogger<LoginCommandHandler> logger, ICommonRepository iCommonRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _iCommonRepository = iCommonRepository;

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
                // var employee = await _unitOfWork.Employees.GetEmployeeInfoForLoginByIdAsync(empId);
                var empInfo = await _unitOfWork.Employees.GetEmployeeByIdAsync(empId);
                GetActiveTenantSubscriptionDetailResquestDTO getActiveTenantSubscriptionDetailResquestDTO = new GetActiveTenantSubscriptionDetailResquestDTO();

                // Null check with fallback to 0
                getActiveTenantSubscriptionDetailResquestDTO.TenantId = empInfo.TenantId ?? 0;

                var subscriptionInfo = await _unitOfWork.TenantSubscriptionRepository
                     .GetTenantActiveSubscriptionPlanDetail(getActiveTenantSubscriptionDetailResquestDTO);

                if (empInfo == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    _logger.LogWarning("Employee may not active or deleted, please contact admin. LoginId: {LoginId}", loginRequest.LoginId);

                    return new ApiResponse<LoginResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "Employee not active. Please contact admin."
                    };
                }

                // ✅ Check if subscription info exists and EndDate > today
                if (subscriptionInfo == null ||
                             !subscriptionInfo.EndDate.HasValue ||
                             subscriptionInfo.EndDate.Value.Date < DateTime.Today)
                             {
                               await _unitOfWork.RollbackTransactionAsync();

                                  _logger.LogWarning("Subscription expired or missing for tenant {TenantId}", getActiveTenantSubscriptionDetailResquestDTO.TenantId);

                            return new ApiResponse<LoginResponseDTO>
                              {
                                 IsSucceeded = false,
                                  Message = "Your subscription has expired. Please contact admin to renew the plan."
                         };
                }

                // Get Active true and Isdeleted false employee

                // 👥 Step 6: Fetch all roles
                var userRoles = await _unitOfWork.UserRoleRepository.GetEmployeeRolesWithDetailsByIdAsync(empId, empInfo.TenantId);

                bool isAdmin = userRoles.Any(ur => ur.Role.RoleType == ConstantValues.TenantAdminRoleType &&   ur.Role.IsSystemDefault == ConstantValues.IsByDefaultFalse &&
                 ur.Role.RoleCode.Equals(ConstantValues.TenantAdminRoleCode, StringComparison.OrdinalIgnoreCase) &&
                 ur.Role.IsActive == true &&
                 ur.Role.IsSoftDeleted == false);
                

                if (isAdmin)
                  {
                      UpdateTenantEnabledOperationFromModuleOperationRequestDTO updateTenantEnabledOperationFromModuleOperationRequestDTO = new UpdateTenantEnabledOperationFromModuleOperationRequestDTO();
                      updateTenantEnabledOperationFromModuleOperationRequestDTO.TenantId = empInfo.TenantId;
                      var updatedDone = _unitOfWork.CommonRepository.UpdateTenantEnabledOperationFromModuleOperationRequestDTO(updateTenantEnabledOperationFromModuleOperationRequestDTO);
                  }



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

                // Getting Tenant Enabled module list
                var TenantEnabledModulesWithOperationData = await _unitOfWork.TenantModuleConfigurationRepository.GetAllTenantEnabledModulesWithOperationsAsync(empInfo.TenantId);

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
                var employeeInfo = _mapper.Map<EmployeeLoginInfoDTO>(empInfo);
                employeeInfo.UserPrimaryRole = primaryRole;
                employeeInfo.UserSecondryRoles = userRoleDTOs;

                // 🧩 Step 9: Load Common Items
                //  var commonItems = await _unitOfWork.CommonRepository.GetCommonItemAsync();

                var parent = await _unitOfWork.ModuleRepository.GetCommonMenuParentAsync();
                 if (parent == null) return null;

               List <ModuleDTO> CommonItems = await _unitOfWork.ModuleRepository.GetCommonMenuTreeAsync(parent.Id);
                var requestDto = new GetActiveRoleModuleOperationsRequestDTO
                {
                    RoleIds = allRoleIdsCsv,
                    TenantId = empInfo.TenantId
                };

                var rolePermissions = await _unitOfWork.CommonRepository
                    .GetActiveRoleModuleOperationsAsync(requestDto);


                // 🔐 Step 10: Fetch Permissions
              
                //  var permissionList = new List<List<RoleModulePermission>> { rolePermissions };

                // 🚀 Step 11: Final Response Object
                var loginResponse = new LoginResponseDTO
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Success = ConstantValues.isSucceeded,
                    EmployeeInfo = employeeInfo,
                    CommonItems = CommonItems,
                    OperationalMenus = rolePermissions,
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
