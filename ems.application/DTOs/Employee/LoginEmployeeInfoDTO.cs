using ems.application.DTOs.ProjectModule;
using ems.application.DTOs.Role;
using ems.application.DTOs.UserRole;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{
    public class EmployeeLoginInfoDTO
    {
        public int EmployeeId { get; set; }  
        public long? TenantId { get; set; }
        public int? DesignationId { get; set; }
        public int? DepartmentId { get; set; }
        public int EmployeeTypeId  { get; set; }
        public string? OfficialEmail  { get; set; }
        public string? EmployeeFullName { get; set; }
        public UserRoleDTO UserPrimaryRole { get; set; }
        public List<UserRoleDTO>? UserSecondryRoles { get; set; }


    }







}

