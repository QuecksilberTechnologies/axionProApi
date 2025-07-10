using AutoMapper;
using ems.application.Constants;
using ems.application.DTOs.SubscriptionModule;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly WorkforceDbContext? _context;
        private readonly ILogger? _logger;
        


        public SubscriptionRepository(WorkforceDbContext context, ILogger<SubscriptionRepository> logger)
        {
            _context = context;
            
            _logger = logger;
        }

        public async Task<int> AddSubscriptionPlanAsync(SubscriptionPlanRequestDTO dto)
        {
            try
            {
                
               
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding subscription plan.");
                throw;
            }
        }

        public async Task<bool> UpdateSubscriptionPlanAsync(int id, SubscriptionPlanRequestDTO dto)
        {
            try
            {
                var plan = await _context.SubscriptionPlans.FindAsync(id);
                if (plan == null) return false;

              
                plan.UpdatedDateTime = DateTime.Now;

                _context.SubscriptionPlans.Update(plan);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating subscription plan.");
                throw;
            }
        }

        public async Task<List<SubscriptionActivePlanDTO>> GetAllPlansAsync()
        {
            try
            {
              //  await using var context = await _contextFactory.CreateDbContextAsync();

                var plans = await _context.SubscriptionPlans
                    .Where(p => p.IsActive)
                    .Select(plan => new SubscriptionActivePlanDTO
                    {
                        Id = plan.Id,
                        PlanName = plan.PlanName,

                        Modules = plan.PlanModuleMappings
                            .Where(pmm => pmm.IsActive ==true && pmm.Module.IsActive==true)
                            .Select(pmm => new ModuleActiveDTO
                            {
                                Id = pmm.Module.Id,
                                ModuleName = pmm.Module.ModuleName,
                                DisplayName = pmm.Module.DisplayName ?? pmm.Module.ModuleName,
                                ParentModuleId = pmm.Module.ParentModuleId ?? 0,

                                //Operations = pmm.Module.ModuleOperationMappings
                                //    .Where(mop => mop.IsActive == true && mop.Operation.IsActive == true)
                                //    .Select(mop => new OperationActiveDTO
                                //    {
                                //        Id = mop.Id,
                                //        DisplayName = mop.Operation.OperationName
                                //    }).ToList()
                            }).ToList()
                    }).ToListAsync();

                // ✅ Nest modules: Parent -> Child
                foreach (var plan in plans)
                {
                    var moduleDict = plan.Modules.ToDictionary(m => m.Id, m => m);
                    var topLevelModules = new List<ModuleActiveDTO>();

                    foreach (var module in plan.Modules)
                    {
                        if (module.ParentModuleId != 0 && moduleDict.TryGetValue(module.ParentModuleId, out var parent))
                        {
                            parent.ChildModules.Add(module);
                        }
                        else
                        {
                            topLevelModules.Add(module);
                        }
                    }

                    plan.Modules = topLevelModules;
                }

                _logger.LogInformation("Fetched {Count} subscription plan(s).", plans.Count);

                return plans;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching subscription plans.");
                return new List<SubscriptionActivePlanDTO>();
            }
        }



        public async Task<SubscriptionPlanResponseDTO> GetPlanByIdAsync(int id)
        {
            var plan = await _context.SubscriptionPlans.FindAsync(id);
            return  null;
        }

    }
    }