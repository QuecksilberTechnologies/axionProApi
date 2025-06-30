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

    public class TenantModuleOperationsUpdateCommand : IRequest<ApiResponse<bool>>
    {
        public TenantModuleOperationsUpdateRequestDTO RequestDTO { get; set; }

        public TenantModuleOperationsUpdateCommand(TenantModuleOperationsUpdateRequestDTO dto)
        {
            RequestDTO = dto;
        }
    }

}
 