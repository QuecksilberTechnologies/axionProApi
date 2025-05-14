using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly WorkforceDbContext? _context;
        private readonly ILogger? _logger;
          public RoleRepository(WorkforceDbContext? context, ILogger<RoleRepository>? logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Role>> CreateRoleAsync(Role role)
        {
            try
            {
                 
                role.AddedDateTime = DateTime.Now; // or DateTime.UtcNow                
                await _context.Roles.AddAsync(role);
                // Changes ko save karenge
                await _context.SaveChangesAsync();

                // Added role ko return karenge
                // `GetAllRolesAsync()` se returned IEnumerable ko List mein convert karenge
                return (await GetAllRolesAsync())
                 .OrderByDescending(r => r.Id) // Latest Role पहले आएगा
                 .ToList();
            }
            catch (Exception ex)
            {
                // Exception ko log karenge
                _logger.LogError(ex, "Error occurred while creating role.");
                throw;  // Rethrow the exception for further handling
            }
        }
        public async Task<List<Role>> GetAllRolesAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all roles from the database...");

                var roles = await _context.Roles.ToListAsync(); // ✅ Corrected EF Core syntax

                if (roles == null || !roles.Any())
                {
                    _logger.LogWarning("No roles found in the database.");
                    return new List<Role>(); // Empty list return karein instead of null
                }

                _logger.LogInformation("Successfully retrieved {Count} roles.", roles.Count);
                return roles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching roles.");
                return new List<Role>(); // Exception ke case me empty list return karein
            }
        }


        public Task<bool> DeleteRoleAsync(int roleId)
        {
            throw new NotImplementedException();
        }

   
        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            try
            {
                // Role ko Id ke basis par find karenge
                var role = await _context.Roles.Where(r => r.Id == roleId).FirstOrDefaultAsync(); // Agar record milta hai toh return karega, nahi toh null

                if (role == null)
                {
                    _logger.LogWarning("Role with ID {RoleId} not found.", roleId);
                }

                return role;   
            }
            catch (Exception ex)
            {
                // Exception ko log karenge
                _logger.LogError(ex, "Error occurred while fetching role with ID {RoleId}.", roleId);
                throw;  // Rethrow the exception for further handling
            }
        }


        public async Task<List<Role>> UpdateRoleAsync(Role role)
        {
            try
            {
                // Role को db से ढूंढेंगे जो हमें update करना है
                var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == role.Id);

                if (existingRole == null)
                {
                    _logger.LogWarning("Role with ID {RoleId} not found.", role.Id);
                    return new List<Role>(); // Agar role nahi milta, to empty list return karenge
                }

                // Existing role ke fields ko update karenge
                existingRole.RoleName = role.RoleName;
                existingRole.Remark = role.Remark;
                existingRole.IsActive = role.IsActive;
                existingRole.UpdatedById = role.UpdatedById; // Assuming Update operation needs tracking
                existingRole.UpdatedDateTime = DateTime.Now; // or DateTime.UtcNow

                // Changes ko save karenge
                await _context.SaveChangesAsync();

                // Updated role ko return karenge
                return (await GetAllRolesAsync()).ToList(); // `GetAllRolesAsync()` ko call karenge taaki updated data fetch ho sake
            }
            catch (Exception ex)
            {
                // Exception ko log karenge
                _logger.LogError(ex, "Error occurred while updating role with ID {RoleId}.", role.Id);
                throw;  // Rethrow the exception for further handling
            }
        }

    }
}
