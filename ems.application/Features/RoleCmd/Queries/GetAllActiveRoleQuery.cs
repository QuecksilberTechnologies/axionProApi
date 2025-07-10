 
using ems.application.DTOs.Role;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.RoleCmd.Queries
{
    public class GetAllActiveRoleQuery : IRequest<ApiResponse<List<RoleResponseDTO>>>
    {
        public GetActiveRoleRequestDTO Dto { get; set; }

        public GetAllActiveRoleQuery(GetActiveRoleRequestDTO dTO)
        {
            Dto = dTO;
        }
    }


}
