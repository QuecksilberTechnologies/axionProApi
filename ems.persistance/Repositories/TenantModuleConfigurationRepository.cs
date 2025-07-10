using Azure;
using Azure.Core;
using ems.application.DTOs.Module;
using ems.application.DTOs.SubscriptionModule;
using ems.application.DTOs.Tenant;
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

        public async Task CreateByDefaultEnabledModulesAsync(  long? tenantId,
            List<TenantEnabledModule> moduleEntities, List<TenantEnabledOperation> operationEntities)
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
        

     
    }

        public async Task<bool> UpdateTenantModuleAndItsOperationsAsync(TenantModuleOperationsUpdateRequestDTO request)
        {
            try
            {
                foreach (var module in request.Modules)
                {
                    // 🟠 1. Update TenantEnabledModule
                    await _context.Database.ExecuteSqlRawAsync(
                        @"UPDATE [AxionPro].[TenantEnabledModule]
                        SET IsEnabled = {0}, UpdatedDateTime = GETUTCDATE()
                        WHERE TenantId = {1} AND ModuleId = {2}",
                        module.IsEnabled, request.TenantId, module.ModuleId
                    );

                    if (module.IsEnabled == false)
                    {
                        //  2. If module is off, then turn off all operations of that module
                        await _context.Database.ExecuteSqlRawAsync(
                            @"UPDATE [AxionPro].[TenantEnabledOperation]
                      SET IsEnabled = 0, UpdatedDateTime = GETUTCDATE()
                      WHERE TenantId = {0} AND ModuleId = {1}",
                            request.TenantId, module.ModuleId
                        );
                    }
                    else
                    {
                        // ✅ 3. If module is enabled, then update only selected operations
                        foreach (var operation in module.Operations)
                        {
                            await _context.Database.ExecuteSqlRawAsync(
                                @"UPDATE [AxionPro].[TenantEnabledOperation]
                          SET IsEnabled = {0}, UpdatedDateTime = GETUTCDATE()
                          WHERE TenantId = {1} AND ModuleId = {2} AND OperationId = {3}",
                                operation.IsEnabled, request.TenantId, module.ModuleId, operation.OperationId
                            );
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bulk update failed while updating module & operations");
                return false;
            }
        }

        //public async Task<List<TenantEnabledModule>> GetAllEnabledTrueModulesWithOperationsByTenantIdAsync(long? tenantId)
        //{
        //    try
        //    {
        //        var modules = await _context.TenantEnabledModules
        //            .Where(m => m.TenantId == tenantId )
        //            .Include(m => m.Module)
        //                .ThenInclude(mod => mod.ModuleOperationMappings
        //                    .Where(mop => mop.IsActive == true)) // Only active mappings
        //            .ToListAsync();

        //        return modules;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error while fetching enabled modules and operations for TenantId: {TenantId}", tenantId);
        //        return new List<TenantEnabledModule>(); // Return empty list on failure
        //    }
        //}



        //yeh function sirf enabled module or operation laata hai , login mei bhi used
        public async Task<List<TenantEnabledModule>> GetAllTenantEnabledModulesWithOperationsAsync(long? tenantId)
        {
            try
            {
                var modules = await _context.TenantEnabledModules
                    .Where(m => m.TenantId == tenantId && m.IsEnabled)
                    .Include(m => m.Module)
                        .ThenInclude(mod => mod.ModuleOperationMappings
                            .Where(mop => mop.IsActive ==true)) // ✅ filtered include EF Core 5+
                    .ToListAsync();

                return modules;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching enabled modules and operations for TenantId: {TenantId}", tenantId);
                return new List<TenantEnabledModule>();
            }
        }



        public async Task<TenantEnabledModuleOperationsResponseDTO> GetAllEnabledTrueModulesWithOperationsByTenantIdAsync(TenantEnabledModuleOperationsRequestDTO tenantEnabledModuleOperationsRequestDTO)
        {
            try
            {
                var tenantId = tenantEnabledModuleOperationsRequestDTO.TenantId;

                // 🟢 Step 1: Get all enabled modules
                var tenantModules = await _context.TenantEnabledModules
                    .Where(t => t.TenantId == tenantId && t.IsEnabled)
                    .Include(t => t.Module)
                    .ThenInclude(m => m.ParentModule)
                    .ToListAsync();

                // 🟢 Step 2: Get all enabled operations
                var tenantOperations = await _context.TenantEnabledOperations
                    .Where(op => op.TenantId == tenantId && op.IsEnabled)
                    .Include(op => op.Operation)
                    .ToListAsync();

                // 🧠 Step 3: Map manually in memory
                var modules = tenantModules.Select(t => new EnabledModuleActiveDTO
                {
                    Id = t.Module.Id,
                    ModuleName = t.Module.ModuleName,
                    ParentModuleId = t.Module.ParentModuleId,
                    ParentModuleName = t.Module.ParentModule != null ? t.Module.ParentModule.ModuleName : "",
                    IsEnabled = t.IsEnabled,
                    Operations = tenantOperations
                        .Where(op => op.ModuleId == t.ModuleId)
                        .Select(op => new EnabledOperationActiveDTO
                        {
                            Id = op.OperationId,
                            OperationName = op.Operation?.OperationName ?? "",
                            IsEnabled = op.IsEnabled
                        }).ToList()
                }).ToList();

                return new TenantEnabledModuleOperationsResponseDTO
                {
                    TenantId = tenantId,
                    Modules = modules
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching enabled modules and operations for tenant.");
                return new TenantEnabledModuleOperationsResponseDTO
                {
                    TenantId = tenantEnabledModuleOperationsRequestDTO.TenantId,
                    Modules = null
                };
            }
        }

        public async Task<TenantEnabledModuleOperationsResponseDTO> GetAllEnabledModulesWithOperationsByTenantIdAsync(TenantEnabledModuleOperationsRequestDTO tenantEnabledModuleOperationsRequestDTO)
        {
            try
            {
                var tenantId = tenantEnabledModuleOperationsRequestDTO.TenantId;

                // Step 1: Get all enabled modules
                var moduleEntities = await _context.TenantEnabledModules
                    .Where(t => t.TenantId == tenantId)
                    .Include(t => t.Module)
                        .ThenInclude(m => m.ParentModule)
                    .ToListAsync();


                // Step 2: Get all operations for this tenant
                var operationEntities = await _context.TenantEnabledOperations
                    .Where(op => op.TenantId == tenantId)
                    .Include(op => op.Operation)
                    .ToListAsync();

                // Step 3: Map to DTO
                var modules = moduleEntities.Select(t => new EnabledModuleActiveDTO
                {
                    Id = t.Module.Id,
                    ModuleName = t.Module.ModuleName,
                    ParentModuleId = t.Module.ParentModuleId,
                    ParentModuleName = t.Module.ParentModule?.ModuleName ?? "",
                    IsEnabled = t.IsEnabled, // ✅ Set from TenantEnabledModules

                    Operations = operationEntities
                        .Where(op => op.ModuleId == t.ModuleId)
                        .Select(op => new EnabledOperationActiveDTO
                        {
                            Id = op.OperationId,
                            OperationName = op.Operation?.OperationName ?? "",
                            IsEnabled = op.IsEnabled // ✅ Set from TenantEnabledOperations
                        }).ToList()
                }).ToList();
                 
                return new TenantEnabledModuleOperationsResponseDTO
                {
                    TenantId = tenantId,
                    Modules = modules
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching enabled modules and operations for tenant.");
                return new TenantEnabledModuleOperationsResponseDTO
                {
                    TenantId = tenantEnabledModuleOperationsRequestDTO.TenantId,
                    Modules = null
                };
            }
        }


    }

}
