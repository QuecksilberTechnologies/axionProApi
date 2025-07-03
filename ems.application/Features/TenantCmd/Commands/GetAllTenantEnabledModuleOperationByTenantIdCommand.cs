using ems.application.DTOs.Tenant;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.RoleCmd.Commands
{
    public class GetAllTenantEnabledModuleOperationByTenantIdCommand : IRequest<ApiResponse<TenantEnabledModuleOperationsResponseDTO>>
    {

        public TenantEnabledModuleOperationsRequestDTO dto { get; set; }

        public GetAllTenantEnabledModuleOperationByTenantIdCommand(TenantEnabledModuleOperationsRequestDTO dto)
        {
            this.dto = dto;
        }

    }
}