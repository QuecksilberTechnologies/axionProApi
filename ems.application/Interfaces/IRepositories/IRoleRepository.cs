using ems.domain.Entity.Masters.RoleInfo;
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

            // Get a role by its Id
            Task<Role> GetRoleByIdAsync(int roleId);

            // Get all roles
            Task<IEnumerable<Role>> GetAllRolesAsync();

            // Update an existing role
            Task<Role> UpdateRoleAsync(Role role);

            // Delete a role by its Id
            Task<bool> DeleteRoleAsync(int roleId);
        }
     

 
}
