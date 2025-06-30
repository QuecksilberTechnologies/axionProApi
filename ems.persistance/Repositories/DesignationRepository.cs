using ems.application.Constants;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
 public   class DesignationRepository : IDesignationRepository
    {
        private WorkforceDbContext _context;
        private ILogger<DesignationRepository> logger;

        public DesignationRepository(WorkforceDbContext context, ILogger<DesignationRepository> logger)
        {
            this._context = context;
            this.logger = logger;
        }

        public async Task<bool> AutoCreateDesignationAsync(Designation designation)
        {
            try
            {
                logger.LogInformation("Attempting to create new Designation for TenantId: {TenantId}", designation.TenantId);

                await _context.Designations.AddAsync(designation);

                var result = await _context.SaveChangesAsync(); // 👈 returns affected rows

                if (result > 0)
                {
                    logger.LogInformation("Designation created successfully with ID: {Id}", designation.Id);
                    return true;
                }
                else
                {
                    logger.LogWarning("No changes were saved while creating Designation for TenantId: {TenantId}", designation.TenantId);
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while creating Designation.");
                throw;
            }
        }


        public async Task<List<Designation>> CreateDesignationAsync(Designation designation)
        {
            try
            {
                

                designation.AddedDateTime = DateTime.Now; // or DateTime.UtcNow                
                await _context.Designations.AddAsync(designation);
                // Changes ko save karenge
                await _context.SaveChangesAsync();

                // Added role ko return karenge
                // `GetAllRolesAsync()` se returned IEnumerable ko List mein convert karenge
                return (await GetAllDesignationAsync(designation.TenantId, designation.IsActive))
                 .OrderByDescending(r => r.Id) // Latest Role पहले आएगा
                 .ToList();
            }
            catch (Exception ex)
            {
                // Exception ko log karenge
                logger.LogError(ex, "Error occurred while creating Designation.");
                throw;  // Rethrow the exception for further handling
            }
        }
        public async Task<bool> DeleteDesignationAsync(Designation designation)
        {
            try
            {
                logger.LogInformation("Attempting to soft delete Designation with ID: {DesignationId}", designation.Id);

                var existingDesignation = await _context.Designations
                    .FirstOrDefaultAsync(d => d.Id == designation.Id
                                            && d.TenantId == designation.TenantId                                            
                                            && d.IsSoftDeleted == false);

                if (existingDesignation == null)
                {
                    logger.LogWarning("Designation with ID {DesignationId} not found for TenantId {TenantId}.", designation.Id, designation.TenantId);
                    return false;
                }

                // Update soft delete fields
                existingDesignation.IsSoftDeleted = ConstantValues.IsByDefaultTrue;
                existingDesignation.IsActive = ConstantValues.IsByDefaultFalse;
                existingDesignation.SoftDeletedById = designation.UpdatedById;
                existingDesignation.SoftDeletedDateTime = DateTime.UtcNow;

                var result = await _context.SaveChangesAsync(); // 👈 returns number of affected rows

                if (result > 0)
                {
                    logger.LogInformation("Designation with ID {DesignationId} soft deleted successfully.", designation.Id);
                    return true;
                }
                else
                {
                    logger.LogWarning("No changes saved while deleting Designation with ID {DesignationId}.", designation.Id);
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while soft deleting Designation with ID {DesignationId}.", designation.Id);
                throw;
            }
        }

        public async Task<List<Designation>> GetAllDesignationAsync(long? tenantId, bool isActive)
        {
            try
            {
                
                logger.LogInformation("Fetching active and non-deleted designations for TenantId: {TenantId}", tenantId);

                var designations = await _context.Designations
                    .Where(d => d.TenantId == tenantId
                                && d.IsActive == isActive
                                && !d.IsSoftDeleted == true)
                    .ToListAsync();

                if (!designations.Any())
                {
                    logger.LogWarning("No active designations found for TenantId: {TenantId}", tenantId);
                }

                return designations;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching designations for TenantId: {TenantId}", tenantId);
                return new List<Designation>();
            }
        }

        public async Task<List<Designation>> GetAllActiveDesignationAsync(long? tenantId)
        {
            try
            {

                logger.LogInformation("Fetching active and non-deleted designations for TenantId: {TenantId}", tenantId);

                var designations = await _context.Designations
                    .Where(d => d.TenantId == tenantId
                                && d.IsActive == true
                                && !d.IsSoftDeleted == true)
                    .ToListAsync();

                if (!designations.Any())
                {
                    logger.LogWarning("No active designations found for TenantId: {TenantId}", tenantId);
                }

                return designations;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching designations for TenantId: {TenantId}", tenantId);
                return new List<Designation>();
            }
        }



        public Task<Designation> GetDesignationByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateDesignationAsync(Designation designation)
        {
            try
            {
                logger.LogInformation("Attempting to update Designation with ID: {DesignationId}", designation.Id);

                var existingDesignation = await _context.Designations
                    .FirstOrDefaultAsync(d => d.Id == designation.Id && d.TenantId == designation.TenantId && d.IsSoftDeleted ==false);

                if (existingDesignation == null)
                {
                    logger.LogWarning("Designation with ID {DesignationId} not found for TenantId {TenantId}.", designation.Id, designation.TenantId);
                    return false;
                }

                // Update relevant fields
                existingDesignation.DesignationName = designation.DesignationName;
                existingDesignation.Description = designation.Description;
                existingDesignation.IsActive = designation.IsActive;
                existingDesignation.UpdatedById = designation.UpdatedById;
                existingDesignation.UpdatedDateTime = DateTime.UtcNow;

                var result = await _context.SaveChangesAsync(); // 👈 return number of affected rows

                if (result > 0)
                {
                    logger.LogInformation("Designation with ID {DesignationId} updated successfully.", designation.Id);
                    return true;
                }
                else
                {
                    logger.LogWarning("No changes saved while updating Designation with ID {DesignationId}.", designation.Id);
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while updating Designation with ID {DesignationId}.", designation.Id);
                throw;
            }
        }

        public async Task<bool> CheckDuplicateValueAsync(long tenantId, string value)
        {
            try
            {
                // Null or empty check
                if (string.IsNullOrWhiteSpace(value) || tenantId <= 0)
                {
                    return false; // Invalid input, cannot check
                }

                // Check if duplicate value exists in Designation table (case-insensitive)
                bool exists = await _context.Designations
              .AnyAsync(d => d.TenantId == tenantId
                          && d.IsSoftDeleted == false
                          && d.DesignationName.ToLower() == value.Trim().ToLower());


                return exists;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while checking duplicate Designation value '{Value}' for TenantId: {TenantId}.", value, tenantId);
                throw;
            }
        }

    }
}
 
