using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
    using ems.domain.Entity;
    using global::ems.domain.Entity;
    using System.Collections.Generic;
    using System.Threading.Tasks;
using ems.application.DTOs.SubscriptionModule;

    namespace ems.application.Interfaces.IRepositories
    {
        public interface ITenantSubscriptionRepository
        {
            /// <summary>
            /// Get subscription detail for a tenant based on filter.
            /// </summary>
            Task<TenantSubscription?> GetTenantSubscriptionPlanInfoAsync(TenantSubscription filter);

            /// <summary>
            /// Get all tenant subscriptions (optional filter).
            /// </summary>
            Task<List<TenantSubscription>> GetAllTenantSubscriptionsAsync(TenantSubscription? filter = null);
   
            Task<GetTenantSubscriptionDetailResponsDTO> GetTenantActiveSubscriptionPlanDetail(GetActiveTenantSubscriptionDetailResquestDTO dto);


        /// <summary>
        /// Add a new tenant subscription.
        /// </summary>
        Task<TenantSubscription> AddTenantSubscriptionAsync(TenantSubscription subscription);

            /// <summary>
            /// Update an existing tenant subscription.
            /// </summary>
            Task UpdateTenantSubscriptionAsync(TenantSubscription subscription);

            /// <summary>
            /// Soft delete a tenant subscription.
            /// </summary>
            Task<bool> DeleteTenantSubscriptionAsync(long id);
        }
    }

 
