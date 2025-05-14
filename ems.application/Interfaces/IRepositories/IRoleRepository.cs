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
           Task<List<Role>> CreateRoleAsync(Role role);

            // Get a role by its Id
            Task<Role> GetRoleByIdAsync(int roleId);

            // Get all roles
            Task<List<Role>> GetAllRolesAsync();

        // Update an existing role
         Task<List<Role>> UpdateRoleAsync(Role role);

            // Delete a role by its Id
            Task<bool> DeleteRoleAsync(int roleId);
        }
     

 
}
