using ems.application.DTOs.Role;
using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{


    public interface IRoleRepository
        {
        // Create a new role
           Task<Role> CreateRoleAsync(Role role);
        
               Task<Role> AutoCreatedSingleTenantRoleAsync(Role role);
               Task<int> AutoCreatedForTenantRoleAsync(List<Role> roles);

        Task<int> AutoCreateUserRoleAndAutomatedRolePermissionMappingAsync(long? TenantId, long employeeId, int role);

        // Get a role by its Id
        Task<Role> GetRoleByIdAsync(int roleId);
        Task<int>GetRoleIdByRoleInfoAsync(Role roleId);

        // Get all roles
        
        Task<List<Role>> GetAllRolesAsync(Role role);
        Task<List<Role>> GetAllActiveRolesAsync(Role role);
     
        Task<List<Role>> GetAllActiveRolesSummaryAsync(long? tenantId);

        // Update an existing role
         Task<Role> UpdateRoleAsync(Role role);

            // Delete a role by its Id
         Task<bool> DeleteRoleAsync(int roleId);
        }
     

 
}
