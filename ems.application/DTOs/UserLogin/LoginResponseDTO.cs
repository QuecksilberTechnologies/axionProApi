using ems.application.DTOs.BasicAndRoleBaseMenuDTO;
using ems.application.DTOs.EmployeeDTO;
using ems.application.DTOs.RoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.UserLogin
{


    public class LoginResponseDTO
    {
        public long Id {get; set;}            
        public bool   Success { get; set; }
        public string? Token { get; set; }
        public string? ExpireWithin { get; set; }       
        public string? RefreshToken { get; set; }
        public LoginEmployeeInfoDTO EmployeeInfo { get; set; }  // Employee Information


    }
}
