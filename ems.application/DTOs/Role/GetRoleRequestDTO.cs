using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Role
{
    public class GetRoleRequestDTO
    {
        public long? TenantId { get; set; }
        public long EmployeeId { get; set; }
        public int RoleId { get; set; }
        public int DesignationId { get; set; }
        
        public bool IsActive{ get; set; } = false;    
    }
}
