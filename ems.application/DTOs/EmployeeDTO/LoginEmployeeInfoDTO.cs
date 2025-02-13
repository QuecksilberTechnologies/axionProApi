using ems.application.DTOs.RoleDTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.EmployeeDTO
{
    public class LoginEmployeeInfoDTO
    {
        public long EmployeeId { get; set; } // Employee ki unique ID

        public string LoginId { get; set; } = string.Empty; // User ka Login ID 

        public string EmployeeFirstName { get; set; } = string.Empty; // User ka Name
        public string EmployeeMiddleName { get; set; } = string.Empty; // User ka Name
        public string EmployeeLastName { get; set; } = string.Empty; // User ka Name
        public string EmployeeFullName { get; set; } = string.Empty; // User ka Name        
        public string EmployeeTypeId { get; set; } = string.Empty; // Employee Type ID
        public string EmployeeType { get; set; } = string.Empty; // Employee Type ID

        public IEnumerable<RoleInfoDTO> EmployeeAssignedRoles { get; set; } = new List<RoleInfoDTO>(); // User ke Roles
    }




}
 
