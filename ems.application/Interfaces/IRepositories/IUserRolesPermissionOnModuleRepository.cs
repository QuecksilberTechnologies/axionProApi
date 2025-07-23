using ems.application.DTOs.BasicAndRoleBaseMenuDTO;
using ems.application.DTOs.RoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface IUserRolesPermissionOnModuleRepository
    {
        public Task<IEnumerable<UserRolesPermissionOnModuleDTO>> GetModuleListAndOperationByRollIdAsync(List<RoleInfoDTO> roleList, int? forPlatform);
    }
}
