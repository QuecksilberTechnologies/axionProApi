﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.BaseDTO
{
    public class PermissionRequestDTO
    {        
            public long? TenantId { get; set; }
            public long EmployeeId { get; set; }
            public int RoleId { get; set; }            
            public int ProjectChildModuleDetailId { get; set; }
            public int OperationId { get; set; }

        
    }
}
