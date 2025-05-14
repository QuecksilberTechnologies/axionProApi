using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Operation
{
    [Keyless]
    public class CheckOperationPermissionRequestDTO
    {
        public string? RoleIds { get; set; }
        public int ProjectChildModuleDetailId { get; set; }
        public int OperationId { get; set; }
        public bool IsActive { get; set; }
        public bool HasAccess { get; set; }

    }

}
