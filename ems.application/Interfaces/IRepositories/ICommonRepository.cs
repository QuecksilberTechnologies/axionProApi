using ems.application.DTOs.Operation;
using ems.application.DTOs.ProjectModule;
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
        Task<int> ValidateUserLoginAsync(string loginId);
        Task<string> UpdateLoginCredential(LoginRequestDTO loginId);
        Task<List<CommonItem>> GetCommonItemAsync();
        Task<List<RoleModulePermission>> GetModulePermissionsAsync(int empId,string roleIds, bool hasAccess, bool isActive);
        Task<bool> GetHasAccessOperation(CheckOperationPermissionRequestDTO checkOperationPermissionRequest);
            
        //   Task  <IUserRoleRepository> UpdateLoginCredential(LoginRequestDTO loginId);
        //  Task List<string> UpdateLoginCredential(LoginRequestDTO loginId);

    }
}
