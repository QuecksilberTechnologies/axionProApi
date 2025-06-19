using ems.application.DTOs.Operation;
using ems.application.DTOs.Tenant;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.RegistrationCmd.Queries
{
     public class GetAllTenantQuery : IRequest<ApiResponse<List<GetAllTenantDTO>>>
    {
        public GetTenantRequestDTO? tenantRequestDTO { get; set; }

        public GetAllTenantQuery(GetTenantRequestDTO tenantRequestDTO)
        {
            this.tenantRequestDTO = tenantRequestDTO;
        }
    }
}
