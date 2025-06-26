using ems.application.DTOs.Module;
using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class TenantModuleConfigurationRepository : ITenantModuleConfigurationRepository
    {
        private readonly WorkforceDbContext? _context;
        private readonly ILogger? _logger;

        public TenantModuleConfigurationRepository(WorkforceDbContext context, ILogger<TenantModuleConfigurationRepository> logger)
        {
            _context = context;

            _logger = logger;
        }

        public async Task CreateDyDefaultEnabledModulesAsync(
         long tenantId,
      List<TenantEnabledModule> moduleEntities,
      List<TenantEnabledOperation> operationEntities)
        {
            try
            {
                // Null/empty check
                if ((moduleEntities == null || !moduleEntities.Any()) &&
                    (operationEntities == null || !operationEntities.Any()))
                {
                    _logger.LogWarning("No module or operation entities to insert for tenantId: {TenantId}", tenantId);
                    return;
                }

                // Insert enabled modules
                if (moduleEntities != null && moduleEntities.Any())
                {
                    await _context.TenantEnabledModules.AddRangeAsync(moduleEntities);
                    _logger.LogInformation("Inserted {Count} enabled modules for tenantId: {TenantId}", moduleEntities.Count, tenantId);
                }

                // Insert enabled operations
                if (operationEntities != null && operationEntities.Any())
                {
                    await _context.TenantEnabledOperations.AddRangeAsync(operationEntities);
                    _logger.LogInformation("Inserted {Count} enabled operations for tenantId: {TenantId}", operationEntities.Count, tenantId);
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation("Tenant default modules and operations saved successfully for tenantId: {TenantId}", tenantId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting default enabled modules/operations for tenantId: {TenantId}", tenantId);
                throw; // Bubble up the error for global handling
            }
        

        // Remove old
        //var oldModules = _context.TenantEnabledModules.Where(x => x.TenantId == tenantId);
        //var oldOps = _context.TenantEnabledOperations.Where(x => x.TenantId == tenantId);
        //_context.TenantEnabledModules.RemoveRange(oldModules);
        //_context.TenantEnabledOperations.RemoveRange(oldOps);
        //await _context.SaveChangesAsync();

        //// Add new
        //var newModules = modules.Select(m => new TenantEnabledModules
        //{
        //    TenantId = tenantId,
        //    ModuleId = m.ModuleId,
        //    IsEnabled = true,
        //    AddedDateTime = DateTime.Now
        //}).ToList();

        //var newOps = modules
        //    .SelectMany(m => m.OperationIds.Select(opId => new TenantEnabledOperations
        //    {
        //        TenantId = tenantId,
        //        ModuleId = m.ModuleId,
        //        OperationId = opId,
        //        IsEnabled = true,
        //        AddedDateTime = DateTime.Now
        //    }))
        //    .ToList();

        //await _context.TenantEnabledModules.AddRangeAsync(newModules);
        //await _context.TenantEnabledOperations.AddRangeAsync(newOps);
        //await _context.SaveChangesAsync();
    }

        public async Task<List<TenantEnabledModule>> GetEnabledModulesWithOperationsAsync(long tenantId)
        {
            try
            {
                var modules = await _context.TenantEnabledModules
                    .Where(m => m.TenantId == tenantId && m.IsEnabled == true)
                    .Include(m => m.Module)
                        .ThenInclude(mod => mod.ModuleOperationMappings
                            .Where(mop => mop.IsActive==true)) // Only active mappings
                    .ToListAsync();

                return modules;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching enabled modules and operations for TenantId: {TenantId}", tenantId);
                return new List<TenantEnabledModule>(); // Return empty list on failure
            }
        }


    }

}
