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


        public async Task<List<Role>> GetAllRolesAsync(Role role)
        {
            try
            {
                _logger.LogInformation("Fetching roles for TenantId: {TenantId}", role.TenantId);

                IQueryable<Role> query = _context.Roles.AsQueryable();

                // ✅ Apply filters
                if (role.TenantId > 0)
                    query = query.Where(r => r.TenantId == role.TenantId);

                query = query.Where(r => r.IsActive == true);

                // ✅ Optional: check for soft delete if column exists
                // query = query.Where(r => r.IsSoftDeleted == false);

                var roles = await query.OrderBy(r => r.RoleName).ToListAsync();

                if (roles == null || !roles.Any())
                {
                    _logger.LogWarning("No roles found for TenantId: {TenantId}", role.TenantId);
                    return new List<Role>();
                }

                _logger.LogInformation("Successfully retrieved {Count} roles.", roles.Count);
                return roles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching roles for TenantId: {TenantId}", role.TenantId);
                return new List<Role>();
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
                var role = await _context.Roles.Where(r => r.Id == roleId && ( r.IsSoftDeleted ==false && r.IsActive ==true)).FirstOrDefaultAsync(); // Agar record milta hai toh return karega, nahi toh null

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
        public async Task<Role> CreateRoleAsync(Role role)
        {
            try
            {
                role.AddedDateTime = DateTime.Now; // or DateTime.UtcNow                
                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();

                // Abhi ye role object updated state mein hai (Id bhi set ho gaya hoga DB se)
                return role;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating role.");
                throw;
            }
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            try
            {
                var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == role.Id);

                if (existingRole == null)
                {
                    _logger.LogWarning("Role with ID {RoleId} not found.", role.Id);
                    return null; // Empty object dena accha practice nahi, isliye null return karo
                }

                existingRole.RoleName = role.RoleName;
                existingRole.Remark = role.Remark;
                existingRole.IsActive = role.IsActive;
                existingRole.UpdatedById = role.UpdatedById;
                existingRole.UpdatedDateTime = DateTime.Now; // or DateTime.UtcNow

                await _context.SaveChangesAsync();

                return existingRole;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating role with ID {RoleId}.", role.Id);
                throw;
            }
        }

        public async Task<Role> AutoCreatedForTenantRoleAsync(Role role)
        {
            try
            {
                if (role == null)
                {
                    _logger.LogWarning("AutoCreatedForTenantRoleAsync: Received null role object.");
                    throw new ArgumentNullException(nameof(role), "Role object cannot be null.");
                }

                // Logging input
                _logger.LogInformation("Creating new Role for TenantId: {TenantId}, RoleName: {RoleName}", role.TenantId, role.RoleName);

                 role.AddedDateTime = DateTime.Now;

                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Role created successfully with Id: {RoleId}", role.Id);

                // Optionally reload from DB if you want latest tracking
                var latestRole = await _context.Roles
                    .OrderByDescending(r => r.Id)
                    .FirstOrDefaultAsync(r => r.Id == role.Id); // ensure you get the one just created

                return latestRole ?? role;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update failed while creating Role.");
                throw; // Let it bubble up or wrap in custom exception
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred in AutoCreatedForTenantRoleAsync.");
                throw;
            }
        }


    }
}
