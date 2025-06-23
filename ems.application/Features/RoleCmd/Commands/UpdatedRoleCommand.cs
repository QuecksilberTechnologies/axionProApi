using ems.application.DTOs.Role;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.RoleCmd.Commands
{
    public class UpdatedRoleCommand : IRequest<ApiResponse<List<RoleResponseDTO>>>
    {
        public UpdateRoleDTO updateRoleDTO { get; set; }

        public UpdatedRoleCommand(UpdateRoleDTO updateRoleDTO)
        {
            this.updateRoleDTO = updateRoleDTO;
        }

    }
    
}
