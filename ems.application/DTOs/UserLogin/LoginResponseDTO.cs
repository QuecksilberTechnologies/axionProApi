using ems.application.DTOs.CommonAndRoleBaseMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.UserLogin
{
    public class LoginResponseDTO
    {
        public bool   Success { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public List<CommonMenuDTO> CommonMenus { get; set; } = new List<CommonMenuDTO>();
        //public List<RolesPermissionDTO> RolesPermissions { get; set; } = new List<RolesPermissionDTO>();
    }
}
