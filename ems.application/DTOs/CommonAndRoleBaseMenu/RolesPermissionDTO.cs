using ems.domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.CommonAndRoleBaseMenu
{
    public class RolesPermissionDTO:BaseEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ModuleId { get; set; }
        public int OperationId { get; set; }
        public bool HasAccess { get; set; }
        public bool IsActive { get; set; }
    }
}
