using ems.domain.Common;
using ems.domain.Entity.RolesPermissionModule;
using ems.domain.Entity.UserRoleModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.domain.Entity.Masters.RoleInfo
{
    public partial class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; } = null!;

        public string? Remark { get; set; }

        public bool IsActive { get; set; }

        public long? AddedById { get; set; }

        public DateTime AddedDateTime { get; set; }

        public long? UpdatedById { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

    }

    }
