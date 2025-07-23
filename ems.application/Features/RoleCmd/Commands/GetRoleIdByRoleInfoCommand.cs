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
    public class GetRoleIdByRoleInfoCommand : IRequest<ApiResponse<int>>
    {
        public GetRoleIdByRoleCodeRequestDTO dto { get; set; }

        public GetRoleIdByRoleInfoCommand(GetRoleIdByRoleCodeRequestDTO dto)
        {
            this.dto = dto;
        }

    }
}
