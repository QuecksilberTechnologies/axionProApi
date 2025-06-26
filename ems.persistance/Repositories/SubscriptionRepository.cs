using AutoMapper;
using ems.application.Constants;
using ems.application.DTOs.SubscriptionModule;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<SubscriptionPlan>> GetAllPlansAsync()
        {
            try
            {
                var result = await _context.SubscriptionPlans
                        .Where(plan => plan.IsActive)
                          .Select(plan => new {
                             plan.Id,
                            plan.PlanName,

                               Modules = plan.PlanModuleMappings
                                 .Where(pmm => pmm.IsActive==true && pmm.Module.IsActive==true)
                                      .Select(pmm => new {
                                          pmm.Module.Id,
                                          pmm.Module.ModuleName,

                                     Operations = pmm.Module.ModuleOperationMappings
                    .Where(op => op.IsActive==true)
                    .Select(op => new {
                        op.Id,
                        op.DisplayName,
                        op.PageUrl
                    }).ToList()
            }).ToList()

    }).ToListAsync();


            //    var result = await _context.SubscriptionPlans
             //  .Where(plan => plan.IsActive).Include(plan => plan.PlanModuleMappings.Where(pmm => pmm.IsActive == true))
              //       .ThenInclude(pmm => pmm.Module).ThenInclude(module => module.ModuleOperationMappings.Where(op => op.IsActive == true)).
               //      Include(pmm => pmm.PlanModuleMappings.Where(op => op.IsActive == true)).ThenInclude(op => op.Module.ModuleOperationMappings.Where(
                //         op => op.IsActive == true)).ToListAsync();


 



 

                _logger.LogInformation("Fetched {Count} subscription plan(s).", result.Count);


                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching subscription plans.");
                return new List<SubscriptionPlan>();
            }
        }


        public async Task<SubscriptionPlanResponseDTO> GetPlanByIdAsync(int id)
        {
            var plan = await _context.SubscriptionPlans.FindAsync(id);
            return  null;
        }

    }
    }