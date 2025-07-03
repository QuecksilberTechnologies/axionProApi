using ems.application.DTOs.Operation;
using ems.application.DTOs.ProjectModule;
using ems.application.DTOs.RoleModulePermission;
using ems.application.DTOs.UserLogin;
using ems.application.DTOs.UserRole;
using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface ICommonRepository
    {
        Task<long> ValidateActiveUserLoginOnlyAsync(string loginId);
        Task<long> ValidateActiveUserCrendentialOnlyAsync(string loginId);

        Task<int> ValidateUserPasswordAsync(string loginId);
        Task<bool> UpdateLoginCredential(LoginRequestDTO loginId);
       

        Task<UpdateTenantEnabledOperationFromModuleOperationResponseDTO> UpdateTenantEnabledOperationFromModuleOperationRequestDTO(
            UpdateTenantEnabledOperationFromModuleOperationRequestDTO request);

        Task<List<RoleModuleOperationResponseDTO>> GetActiveRoleModuleOperationsAsync( GetActiveRoleModuleOperationsRequestDTO request);
        

          Task<bool> GetHasAccessOperation(CheckOperationPermissionRequestDTO checkOperationPermissionRequest);
        Task<bool> HasPermissionAsync(long userId, string permissionCode);
        Task<bool> IsTenantValidAsync(long userId, long? TenantId);


        //   Task  <IUserRoleRepository> UpdateLoginCredential(LoginRequestDTO loginId);
        //  Task List<string> UpdateLoginCredential(LoginRequestDTO loginId);

    }
}
