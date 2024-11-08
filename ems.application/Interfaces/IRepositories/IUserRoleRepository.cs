using ems.domain.Entity.UserRoleModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface IUserRoleRepository
    {
        // Get role name by user ID (for your login process)
        Task<string> GetUsersRoleByIdAsync(int userId);

        // CRUD Operations
       // Task<UserRole> GetUserRoleByIdAsync(int id);
        Task<IEnumerable<UserRole>> GetAllUserRolesAsync();
        Task AddUserRoleAsync(UserRole userRole);
        Task UpdateUserRoleAsync(UserRole userRole);
        Task DeleteUserRoleAsync(int id);
    }
     

}
