using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly WorkforceDbContext _context;
        private readonly ILogger<DepartmentRepository> _logger;

        public DepartmentRepository(WorkforceDbContext context, ILogger<DepartmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Department?> GetByIdAsync(long id)
        {
            try
            {
                return await _context.Departments.FirstOrDefaultAsync(d => d.Id == id && !d.IsSoftDeleted == true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching department by Id {Id}", id);
                return null;
            }
        }

        public async Task<List<Department>> GetAllByTenantAsync(long tenantId)
        {
            try
            {
                return await _context.Departments
                    .Where(d => d.TenantId == tenantId && !d.IsSoftDeleted == true)
                    .OrderBy(d => d.DepartmentName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching departments for TenantId: {TenantId}", tenantId);
                return new List<Department>();
            }
        }

        public async Task<List<Department>> GetAllActiveAsync(long? tenantId)
        {
            try
            {
                return await _context.Departments
                    .Where(d => d.TenantId == tenantId && d.IsActive == true && !d.IsSoftDeleted == true)
                    .OrderBy(d => d.DepartmentName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching active departments for TenantId: {TenantId}", tenantId);
                return new List<Department>();
            }
        }

        public async Task<long> CreateAsync(Department department)
        {
            try
            {
                if (department == null)
                {
                    _logger.LogWarning("CreateAsync called with null department object.");
                    throw new ArgumentNullException(nameof(department));
                }

                await _context.Departments.AddAsync(department);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Department created successfully with Id: {Id}", department.Id);
                return department.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating department.");
                return -1;
            }
        }

        public async Task<bool> UpdateAsync(Department department)
        {
            try
            {
                var existing = await _context.Departments.FirstOrDefaultAsync(d => d.Id == department.Id && !d.IsSoftDeleted == true);
                if (existing == null)
                {
                    _logger.LogWarning("Update failed: Department not found for Id: {Id}", department.Id);
                    return false;
                }

                existing.DepartmentName = department.DepartmentName;
                existing.Description = department.Description;
                existing.Remark = department.Remark;
                existing.IsActive = department.IsActive;
                existing.TenantIndustryId = department.TenantIndustryId;
                existing.UpdatedById = department.UpdatedById;
                existing.UpdatedDateTime = DateTime.Now;

                _context.Departments.Update(existing);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Department updated successfully. Id: {Id}", department.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating department.");
                return false;
            }
        }

        public async Task<bool> SoftDeleteAsync(long id, long deletedById)
        {
            try
            {
                var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id && !d.IsSoftDeleted == true);
                if (department == null)
                {
                    _logger.LogWarning("SoftDelete failed: Department not found for Id: {Id}", id);
                    return false;
                }

                department.IsSoftDeleted = true;
                department.DeletedById = deletedById;
                department.DeletedDateTime = DateTime.Now;

                _context.Departments.Update(department);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Department soft-deleted successfully. Id: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while soft deleting department. Id: {Id}", id);
                return false;
            }
        }

        public async Task<bool> ExistsAsync(long id, long tenantId)
        {
            try
            {
                return await _context.Departments.AnyAsync(d => d.Id == id && d.TenantId == tenantId && !d.IsSoftDeleted == true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of department. Id: {Id}", id);
                return false;
            }
        }
        public async Task<Dictionary<string, int>> GetDepartmentNameIdMapAsync(long tenantId)
        {
            return await _context.Departments
                .Where(d => !d.IsSoftDeleted==true && d.TenantId == tenantId)
                .ToDictionaryAsync(d => d.DepartmentName, d => d.Id);
        }


        public async Task<int> AutoCreateDepartmentSeedAsync(List<Department>? departments)
        {
            try
            {
                if (departments == null || !departments.Any())
                {
                    _logger.LogWarning("Department seed list is null or empty. Seeding aborted.");
                    return -1;
                }

                await _context.Departments.AddRangeAsync(departments);
                int insertedCount = await _context.SaveChangesAsync();

                if (insertedCount != departments.Count)
                {
                    _logger.LogError("Mismatch in inserted department count. Expected: {Expected}, Inserted: {Inserted}", departments.Count, insertedCount);
                    return -1;
                }

                long tenantId = departments.FirstOrDefault()?.TenantId ?? 0;

                // ✅ Fetch the inserted Executive Office department's ID
                var executiveOfficeDeptId = await _context.Departments
                    .Where(d => d.IsExecutiveOffice ==true && !d.IsSoftDeleted==true && d.TenantId== tenantId)                  
                    .Select(d => d.Id)
                    .FirstOrDefaultAsync();

                if (executiveOfficeDeptId == 0)
                {
                    _logger.LogWarning("Executive Office department not found after insertion.");
                    return -1;
                }

                _logger.LogInformation("Successfully inserted {Count} departments. Executive Office DepartmentId: {ExecutiveOfficeId}", insertedCount, executiveOfficeDeptId);
                return executiveOfficeDeptId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while inserting department seed data.");
                return -1;
            }
        }

    }

}
