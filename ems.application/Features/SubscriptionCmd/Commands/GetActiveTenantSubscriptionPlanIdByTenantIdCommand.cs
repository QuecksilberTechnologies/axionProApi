using ems.application.DTOs.SubscriptionModule;
using ems.application.DTOs.Tenant;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.SubscriptionCmd.Commands
{
    public class GetActiveTenantSubscriptionPlanIdByTenantIdCommand :IRequest<ApiResponse<GetTenantSubscriptionDetailResponsDTO>>
    {

        public GetActiveTenantSubscriptionDetailResquestDTO dto { get; set; }

    public GetActiveTenantSubscriptionPlanIdByTenantIdCommand(GetActiveTenantSubscriptionDetailResquestDTO dto)
    {
        this.dto = dto;
    }

}
}
