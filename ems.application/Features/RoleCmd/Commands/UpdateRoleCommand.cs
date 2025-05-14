
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
    public class UpdateRoleCommand : IRequest<ApiResponse<List<GetAllRoleDTO>>>
    {
        public UpdateRoleDTO updateRoleDTO { get; set; }

        public UpdateRoleCommand(UpdateRoleDTO updateRoleDTO)
        {
            this.updateRoleDTO = updateRoleDTO;
        }

    }
}
