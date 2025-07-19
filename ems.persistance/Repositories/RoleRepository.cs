using ems.application.Constants;
using ems.application.DTOs.Role;
using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
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

        public async Task<int> AutoCreateUserRoleAndAutomatedRolePermissionMappingAsync(long? tenantId, long employeeId, int role)
        {
            try
            { 

                // 3. Fetch TenantEnabledOperation list for that tenant
                var enabledOperations = await _context.TenantEnabledOperations
                    .Where(x => x.TenantId == tenantId)
                    .ToListAsync();

                // 4. Convert to RoleModuleAndPermission
                var rolePermissions = enabledOperations.Select(op => new RoleModuleAndPermission
                {
                    RoleId = role,
                    ModuleId = op.ModuleId,
                    OperationId = op.OperationId,
                    HasAccess = true,
                    IsActive = op.IsEnabled,
                    Remark = "System Genrate Prmission for user",
                    IsOperational = true,
                    AddedById = tenantId,
                    AddedDateTime = DateTime.Now,
                    IsSoftDeleted = false,

                }).ToList();

                await _context.RoleModuleAndPermissions.AddRangeAsync(rolePermissions);

                // Final Save
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AutoCreateRoleUserRoleAndAutomatedRolePermissionMappingAsync");
                throw;
            }
        }

     
        public async Task<List<Role>> GetAllActiveRolesSummaryAsync(long? tenantId)
        {
            try
            {
                _logger.LogInformation("Fetching roles for TenantId: {TenantId}", tenantId);

                IQueryable<Role> query = _context.Roles.AsQueryable();

                // ✅ Apply filters
                if (tenantId > 0)
                    query = query.Where(r => r.TenantId == tenantId);
                    query = query.Where(r => r.IsActive == true && r.IsSoftDeleted == false);

                // ✅ Optional: check for soft delete if column exists
                // query = query.Where(r => r.IsSoftDeleted == false);

                var roles = await query.OrderBy(r => r.RoleName).ToListAsync();

                if (roles == null || !roles.Any())
                {
                    _logger.LogWarning("No roles found for TenantId: {TenantId}", tenantId);
                    return new List<Role>();
                }

                _logger.LogInformation("Successfully retrieved {Count} roles.", roles.Count);
                return roles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching roles for TenantId: {TenantId}", tenantId);
                return new List<Role>();
            }
        }


        public async Task<List<Role>> GetAllActiveRolesAsync(Role role)
        {
            try
            {
                _logger.LogInformation("Fetching roles for TenantId: {TenantId}", role.TenantId);

                IQueryable<Role> query = _context.Roles.AsQueryable();

                // ✅ Apply filters
                if (role.TenantId > 0)
                    query = query.Where(r => r.TenantId == role.TenantId);
                    query = query.Where(r => r.IsActive == true && r.IsSoftDeleted ==false);

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

        public async Task<List<Role>> GetAllRolesAsync(Role role)
        {
            try
            {
                _logger.LogInformation("Fetching roles for TenantId: {TenantId}", role.TenantId);

                IQueryable<Role> query = _context.Roles.AsQueryable();

                // ✅ Apply filters
                if (role.TenantId > 0)
                    query = query.Where(r => r.TenantId == role.TenantId);
                query = query.Where(r => r.IsActive == role.IsActive && r.IsSoftDeleted == false);

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

        public async Task<int> GetRoleIdByRoleInfoAsync(Role role)
        {
            try

            {
                var roleInfo = await _context.Roles.
                    Where(r =>
                        (role.TenantId == 0 ? r.TenantId == null : r.TenantId == role.TenantId) &&
                        r.IsSoftDeleted == false &&
                        r.IsActive == true &&
                        r.IsSystemDefault == true &&
                        r.RoleType == role.RoleType &&
                        r.RoleCode == role.RoleCode)
                    .FirstOrDefaultAsync();

                if (roleInfo == null)
                {
                    _logger.LogWarning("Role not found with RoleCode: {RoleCode}, RoleType: {RoleType}", role.RoleCode, role.RoleType);
                    return 0;
                }

                return roleInfo.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching role with RoleCode: {RoleCode}", role.RoleCode);
                return 0;
            }
        }

        public async Task<int> GetRoleIdByRoleCodeAsync(string roleCode, string roleType)
        {
            try
            {
                var roleInfo = await _context.Roles
                    .Where(r =>
                        r.TenantId == null &&
                        r.IsSoftDeleted == false &&
                        r.IsActive == true &&
                        r.IsSystemDefault == true &&
                        r.RoleType == roleType &&
                        r.RoleCode == roleCode)
                    .FirstOrDefaultAsync();

                if (roleInfo == null)
                {
                    _logger.LogWarning("Role not found with RoleCode: {RoleCode}, RoleType: {RoleType}", roleCode, roleType);
                    return 0;
                }

                return roleInfo.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching role with RoleCode: {RoleCode}", roleCode);
                return 0;
            }
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
                role.RoleType = "Tenant";
                role.RoleCode = "Tenant-User";
                role.IsSystemDefault = false;                         
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
                var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == role.Id && r.IsSoftDeleted==false && r.TenantId== role.TenantId);

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

        public async Task<Role> AutoCreatedSingleTenantRoleAsync(Role role)
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

        public async Task<int> AutoCreatedForTenantRoleAsync(List<Role> roles)
        {
            try
            {
                if (roles == null || !roles.Any())
                {
                    _logger.LogWarning("AutoCreatedForTenantRoleAsync: Received null or empty role list.");
                    throw new ArgumentNullException(nameof(roles), "Role list cannot be null or empty.");
                }

                foreach (var role in roles)
                {
                    _logger.LogInformation("Creating Role for TenantId: {TenantId}, RoleName: {RoleName}", role.TenantId, role.RoleName);
                    role.AddedDateTime = DateTime.Now;
                }

                await _context.Roles.AddRangeAsync(roles);
                var insertedCount = await _context.SaveChangesAsync();

                _logger.LogInformation("{Count} Role(s) saved in DB.", insertedCount);

                // 🔍 Each Role insert = 1 row affected → insertedCount should match roles.Count
                if (insertedCount != roles.Count)
                {
                    _logger.LogError("Mismatch in role insert count. Expected: {Expected}, Inserted: {Inserted}",
                                     roles.Count, insertedCount);
                    return -1;
                }

                // 🔍 Return only the Tenant-Admin Role Id
                var tenantAdminRole = await _context.Roles
                    .Where(r => r.RoleCode == ConstantValues.TenantAdminRoleCode && !r.IsSoftDeleted==true && r.IsActive==true)
                    .OrderByDescending(r => r.Id)
                    .FirstOrDefaultAsync();

                if (tenantAdminRole != null)
                {
                    _logger.LogInformation("Tenant-Admin role created successfully with Id: {Id}", tenantAdminRole.Id);
                    return tenantAdminRole.Id;
                }

                _logger.LogWarning("Tenant-Admin role not found after insert.");
                return -1;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update failed while creating roles.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred in AutoCreatedForTenantRoleAsync.");
                throw;
            }
        }

      
      
    }
}
