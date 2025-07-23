 
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

    public class CreateRoleCommand : IRequest<ApiResponse<RoleResponseDTO>>
     {
        public CreateRoleDTO createRoleDTO { get; set; }

        public CreateRoleCommand(CreateRoleDTO createRoleDTO)
        {
            this.createRoleDTO = createRoleDTO;
        }

    }




}
