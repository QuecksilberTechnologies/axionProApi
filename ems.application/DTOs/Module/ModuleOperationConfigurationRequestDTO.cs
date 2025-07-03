using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Module
{
    public class ModuleOperationConfigurationRequestDTO
    {
     public   long? TenantId { get; set; }

     public   int RoleId { get; set; }    

     public   long EmployeeId { get; set; }    
    }
}
