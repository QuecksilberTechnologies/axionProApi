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
        
            Task<Role> AutoCreatedForTenantRoleAsync(Role role);
            Task<int> AutoCreateUserRoleAndAutomatedRolePermissionMappingAsync(long? TenantId, long employeeId, Role role);

        // Get a role by its Id
        Task<Role> GetRoleByIdAsync(int roleId);
        Task<int>GetRoleIdByRoleInfoAsync(Role roleId);

            // Get all roles
        Task<List<Role>> GetAllRolesAsync(Role role);

        // Update an existing role
         Task<Role> UpdateRoleAsync(Role role);

            // Delete a role by its Id
         Task<bool> DeleteRoleAsync(int roleId);
        }
     

 
}
