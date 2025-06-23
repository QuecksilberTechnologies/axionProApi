using AutoMapper;
using ems.application.DTOs.SubscriptionModule;
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
               

                IQueryable<SubscriptionPlan> query = _context.SubscriptionPlans.AsQueryable();
                var plans = await _context.SubscriptionPlans.Include(p => p.PlanModuleMappings).ThenInclude(pm => pm.Module).ToListAsync();


                // ✅ IsActive is always mandatory
                query = query.Where(p => p.IsActive == true);

                // ✅ Optional filters only if subscriptionPlan is not null

                
               
                var plans_ = await query.OrderByDescending(p => p.AddedDateTime).ToListAsync();

                _logger.LogInformation("Fetched {Count} subscription plan(s).", plans.Count);

                return plans;
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