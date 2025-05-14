using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Role
{
    public class UpdateRoleDTO
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string? Remark { get; set; } // Nullable
        public bool IsActive { get; set; } = false; // Default false


        public long? UpdatedById { get; set; } // Nullable
        public DateTime UpdatedDateTime { get; set; } = DateTime.UtcNow; // Default value
    }
}
