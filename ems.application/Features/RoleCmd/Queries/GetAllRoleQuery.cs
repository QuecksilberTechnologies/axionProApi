 
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
    public class GetAllRoleQuery : IRequest<ApiResponse<List<RoleResponseDTO>>>
    {
        public RoleRequestDTO RoleRequest { get; set; }

        public GetAllRoleQuery(RoleRequestDTO roleRequest)
        {
            RoleRequest = roleRequest;
        }
    }


}
