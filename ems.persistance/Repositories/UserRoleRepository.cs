using ems.application.Interfaces.IRepositories;
using ems.domain.Entity.EmployeeModule;
using ems.domain.Entity.UserRoleModule;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly EmsDbContext _context;
        private readonly ILogger<UserRoleRepository>? _logger;

        public UserRoleRepository(EmsDbContext context, ILogger<UserRoleRepository>? logger)
        {
            _context = context;
             _logger = logger;
        }

        public async Task<string> GetUsersRoleByIdAsync(int userId)
        {
            try
            {
                // _logger.LogInformation("Fetching role for user with ID: {UserId}", userId);

                // Fetching the user role with its related role details
                var userRole = await _context.UserRoles
                                       .FirstOrDefaultAsync(ur => ur.EmployeeId == userId);


                // Check if the userRole exists; if not, log and return null
                if (userRole == null || userRole.Id == null)
                {
                    _logger.LogWarning("No role found for user with ID: {UserId}", userId);
                    return null;
                }

                return userRole.Remark;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the role for user with ID: {UserId}", userId);
                throw;
            }
        }

        public async Task AddUserRoleAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserRoleAsync(int id)
        {
            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserRole>> GetAllUserRolesAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<UserRole> GetUserRoleByIdAsync(int id)
        {
            return await _context.UserRoles.FindAsync(id);
        }

        public async Task UpdateUserRoleAsync(UserRole userRole)
        {
            _context.UserRoles.Update(userRole);
            await _context.SaveChangesAsync();
        }

        
    }
}
