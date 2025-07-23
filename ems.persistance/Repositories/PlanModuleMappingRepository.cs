using ems.application.DTOs.Module;
using ems.application.DTOs.Tenant;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class PlanModuleMappingRepository : IPlanModuleMappingRepository
    {
        private readonly WorkforceDbContext? _context;
        private readonly ILogger? _logger;



        public PlanModuleMappingRepository(WorkforceDbContext context, ILogger<PlanModuleMappingRepository> logger)
        {
            _context = context;

            _logger = logger;
        }

        public Task AddAsync(PlanModuleMapping planModuleMapping)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<PlanModuleMappingResponseDTO> GetModulesBySubscriptionPlanIdAsync(int? subscriptionPlanId)
        {
            try
            {
                if (subscriptionPlanId == null)
                {
                    return new PlanModuleMappingResponseDTO
                    {
                        SubscriptionPlanId = 0,
                        Modules = new List<ModuleWithOperationsDTO>()
                    };
                }

                var mappings = await _context.PlanModuleMappings
                    .Where(p => p.SubscriptionPlanId == subscriptionPlanId && p.IsActive == true)
                    .Include(p => p.Module)
                        .ThenInclude(m => m.ModuleOperationMappings)
                            .ThenInclude(mop => mop.Operation)
                    .Include(p => p.Module)
                        .ThenInclude(m => m.ParentModule) // ✅ Include Parent Module
                    .ToListAsync();

                var response = new PlanModuleMappingResponseDTO
                {
                    SubscriptionPlanId = subscriptionPlanId.Value,
                    Modules = mappings
                        .Where(p => p.Module != null)
                        .Select(p => new ModuleWithOperationsDTO
                        {
                            ModuleId = p.Module.Id,
                            ModuleName = p.Module.ModuleName,
                            DisplayName = p.Module.DisplayName?.ToString()?? string.Empty,
                            ParentModuleId = p.Module.ParentModuleId,
                            MainModuleId = p.Module.ParentModule?.Id ?? 0,
                            MainModuleName = p.Module.ParentModule?.ModuleName ?? string.Empty,
                            Operations = p.Module.ModuleOperationMappings
                               .Where(mop =>
                                        mop.IsActive == true &&
                                        mop.Operation != null/* &&
                                        mop.Operation.IsActive == true*/)
                                       .Select(mop => new OperationResponseDTO
                                            {
                                         OperationId = mop.Operation.Id,
                                        
                                          //PageUrl = mop.PageUrl,
                                          //IconUrl = mop.IconUrl
                                           })
                                      .ToList()

                        })
                        .ToList()
                };

                return response;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching modules and operations for SubscriptionPlanId: {SubscriptionPlanId}", subscriptionPlanId);

                return new PlanModuleMappingResponseDTO
                {
                    SubscriptionPlanId = subscriptionPlanId ?? 0,
                    Modules = new List<ModuleWithOperationsDTO>()
                };
            }
        }



        public Task<bool> IsModuleMappedAsync(int subscriptionPlanId, int moduleId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PlanModuleMapping planModuleMapping)
        {
            throw new NotImplementedException();
        }

        
    }
}
