using ems.domain.Entity.RoleModulePermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.domain.Entity.Masters.ModuleOperation
{
    public partial class Operation
    {
        public int Id { get; set; }

        public string OperationName { get; set; } = null!;

        public string? Remark { get; set; }

        public bool? IsActive { get; set; }

        public long? AddedById { get; set; }

        public DateTime AddedDateTime { get; set; }

        public long? UpdatedById { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<RoleModuleAndPermission> RoleModuleAndPermissions { get; set; } = new List<RoleModuleAndPermission>();
    }


}
