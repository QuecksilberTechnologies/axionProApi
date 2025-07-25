﻿using ems.application.DTOs.SubscriptionModule;
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
    public class TenantSubscriptionRepository : ITenantSubscriptionRepository
    {
        private readonly WorkforceDbContext? _context;
        private readonly ILogger? _logger;
        public TenantSubscriptionRepository(WorkforceDbContext? context, ILogger<TenantSubscriptionRepository>? logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TenantSubscription> AddTenantSubscriptionAsync(TenantSubscription subscription)
        {
            try
            {
                 

                await _context.TenantSubscriptions.AddAsync(subscription);
                await _context.SaveChangesAsync();

                _logger.LogInformation("✅ TenantSubscription inserted successfully for TenantId: {TenantId}", subscription.TenantId);

                return subscription;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error while inserting TenantSubscription for TenantId: {TenantId}", subscription.TenantId);
                throw;
            }
        }


        public Task<bool> DeleteTenantSubscriptionAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TenantSubscription>> GetAllTenantSubscriptionsAsync(TenantSubscription? filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task<GetTenantSubscriptionDetailResponsDTO> GetTenantActiveSubscriptionPlanDetail(GetActiveTenantSubscriptionDetailResquestDTO dto)
        {
            var subscription = await _context.TenantSubscriptions
                .Where(x => x.TenantId == dto.TenantId && x.IsActive)
                .Select(x => new GetTenantSubscriptionDetailResponsDTO
                {
                    TenantId = x.TenantId,
                    TenantSubscriptionId =x.SubscriptionPlanId,
                    PlanName = x.SubscriptionPlan.PlanName,         // assuming navigation property exists
                    StartDate = x.SubscriptionStartDate,
                    EndDate = x.SubscriptionEndDate,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();

            return subscription;
        }


        public async Task<TenantSubscription?> GetTenantSubscriptionPlanInfoAsync(TenantSubscription filter)
        {
            try
            {
                if (filter == null || filter.TenantId <= 0)
                {
                    _logger.LogWarning("Invalid filter passed to GetTenantSubscriptionAsync.");
                    return null;
                }

                _logger.LogInformation("Fetching TenantSubscription for TenantId: {TenantId}", filter.TenantId);

                var query = _context.TenantSubscriptions
                    .Where(x => x.TenantId == filter.TenantId && x.IsActive == true);

                //if (filter.SubscriptionPlanId > 0)
                //    query = query.Where(x => x.SubscriptionPlanId == filter.SubscriptionPlanId);

                //if (filter.IsActive.HasValue)
                //    query = query.Where(x => x.IsActive == filter.IsActive);

                var result = await query.FirstOrDefaultAsync();

                _logger.LogInformation("TenantSubscription record {Status} for TenantId: {TenantId}",
                                       result != null ? "found" : "not found", filter.TenantId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching TenantSubscription.");
                return null;
            }
        }


        public Task UpdateTenantSubscriptionAsync(TenantSubscription subscription)
        {
            throw new NotImplementedException();
        }
    }
}
