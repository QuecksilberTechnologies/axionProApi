using ems.application.DTOs.BasicAndRoleBaseMenuDTO;
using ems.application.DTOs.UserLogin;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.UserLoginAndDashboardCmd.Commands
{
    public class UserRolesPermissionOnModuleCommand : IRequest<ApiResponse<IEnumerable<UserRolesPermissionOnModuleDTO>>>
    {

        public AccessDetailRequestDTO AccessDetailDTO { get; set; }
        public UserRolesPermissionOnModuleCommand(AccessDetailRequestDTO accessRequestDTO)
        {
            AccessDetailDTO = accessRequestDTO;
        }

    
    }
}
