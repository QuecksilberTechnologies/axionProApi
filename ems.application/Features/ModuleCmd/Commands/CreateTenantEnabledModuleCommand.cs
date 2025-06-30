using ems.application.DTOs.Tenant;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.ModuleCmd.Commands
{
    public class CreateTenantEnabledModuleCommand : IRequest<ApiResponse<bool>>
    {

        public TenantEnableModuleDTO tenantEnableModuleRequest { get; set; }

        public CreateTenantEnabledModuleCommand(TenantEnableModuleDTO createModuleRequestDTO)
        {
            this.tenantEnableModuleRequest = createModuleRequestDTO;
        }

    }
}
 