using ems.domain.Common;
using ems.domain.Entity.Masters.ProjectModuleInfo;
using ems.domain.Entity.Masters.ModuleOperation;
using ems.domain.Entity.Masters.RoleInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ems.domain.Entity.BasicMenuInfo;

namespace ems.domain.Entity.RoleModulePermission
{

    public partial class RoleModuleAndPermission
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public int SubModuleId { get; set; }

        public int OperationId { get; set; }

        public bool HasAccess { get; set; }

        public bool IsActive { get; set; }

        public string? Remark { get; set; }

        public byte[]? ImageIcon { get; set; }

        public long AddedById { get; set; }

        public DateTime AddedDateTime { get; set; }

        public long? UpdatedById { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public virtual ProjectSubModuleDetail SubModule { get; set; } = null!;

        public virtual Operation Operations { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
    }


}
