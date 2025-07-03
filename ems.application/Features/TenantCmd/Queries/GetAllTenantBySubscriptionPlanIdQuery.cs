using ems.application.DTOs.Operation;
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
    public class GetAllTenantBySubscriptionPlanIdQuery : IRequest<ApiResponse<List<TenantResponseDTO>>>
    {
        public TenantRequestDTO? tenantRequestDTO { get; set; }

        public GetAllTenantBySubscriptionPlanIdQuery(TenantRequestDTO tenantRequestDTO)
        {
            this.tenantRequestDTO = tenantRequestDTO;
        }
    }
}
