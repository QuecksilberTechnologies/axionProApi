using ems.application.DTOs.SubscriptionModule;
using ems.application.DTOs.Tenant;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.TenantCmd.Commands
{
    public class GetAllTenantTrueEnabledModuleOperationByTenantIdCommand : IRequest<ApiResponse<TenantEnabledModuleOperationsResponseDTO>>
    {

        public TenantEnabledModuleOperationsRequestDTO TenantEnabledModuleOperationsRequestDTO { get; set; }

        public GetAllTenantTrueEnabledModuleOperationByTenantIdCommand(TenantEnabledModuleOperationsRequestDTO TenantEnabledModuleOperationsRequestDTO)
        {
            this.TenantEnabledModuleOperationsRequestDTO = TenantEnabledModuleOperationsRequestDTO;
        }

    }
}
