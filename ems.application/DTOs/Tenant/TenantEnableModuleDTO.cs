using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Tenant
{
    public class TenantEnableModuleDTO
    {
        public int TenantId { get; set; }
        public List<TenantModuleDTO> Modules { get; set; } = new();
    }

    public class TenantModuleDTO
    {
        public int ModuleId { get; set; }
        public List<int> OperationIds { get; set; } = new(); // Optional
    }

}
