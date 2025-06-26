using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Module
{
    public class TenantModuleUpdateDTO
    {
        public long TenantId { get; set; }
        public List<ModuleToggleDTO> Modules { get; set; } = new();
        public List<OperationToggleDTO> Operations { get; set; } = new();
    }

    public class ModuleToggleDTO
    {
        public int ModuleId { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class OperationToggleDTO
    {
        public int ModuleId { get; set; }
        public int OperationId { get; set; }
        public bool IsEnabled { get; set; }
    }

     

}
