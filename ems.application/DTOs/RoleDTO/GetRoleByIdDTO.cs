using ems.domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.RoleDTO
{
    public class GetRoleByIdDTO
    {
        public int Id { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public string? Remark { get; set; }

        public bool IsActive { get; set; }

        // Agar `AddedDateTime` aur `UpdatedDateTime` bhi chahiye, toh unhe bhi include kar sakte hain
        public DateTime AddedDateTime { get; set; }

        public DateTime? UpdatedDateTime { get; set; }
    }

}
