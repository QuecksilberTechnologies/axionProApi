 using ems.application.DTOs.Employee;
using ems.application.DTOs.Module;
using ems.application.DTOs.ProjectModule;
using ems.application.DTOs.RoleModulePermission;
using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.UserLogin
{


    public class LoginResponseDTO
    {
                   
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? Message { get; set; }
        public EmployeeLoginInfoDTO? EmployeeInfo { get; set; }
        public string? Allroles { get; set; }

        public List<ModuleDTO>? CommonItems { get; set; }
        public List<RoleModuleOperationResponseDTO>? OperationalMenus { get; set; }
        




    }
}
