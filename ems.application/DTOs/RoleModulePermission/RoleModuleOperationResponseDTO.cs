using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.RoleModulePermission
{
    [Keyless]
    public class RoleModuleOperationResponseDTO
    {
        public int RoleId { get; set; }
        public long? TenantId { get; set; }

        public int ModuleId { get; set; }
        public string? SubModuleName { get; set; }

        public int? ParentModuleId { get; set; }
        public string? ParentModuleName { get; set; }

        public int OperationId { get; set; }
        public string? OperationName { get; set; }

        public string? DisplayName { get; set; }
        public string? PageURL { get; set; }
        public string? IconURL { get; set; }
    }

}
