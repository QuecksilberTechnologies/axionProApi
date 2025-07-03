using ems.application.DTOs.Tenant;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.RoleCmd.Queries
{
    public class GetTenantSubscriptionQuery : IRequest<ApiResponse<TenantSubscriptionPlanResponseDTO>>
    {
        public TenantSubscriptionPlanRequestDTO? tenantSubscriptionPlanRequest { get; set; }

        public GetTenantSubscriptionQuery(TenantSubscriptionPlanRequestDTO tenantSubscriptionPlanRequest)
        {
            this.tenantSubscriptionPlanRequest = tenantSubscriptionPlanRequest;
        }
    }
}
