using ems.domain.Entity.EmployeeModule;
using ems.domain.Entity.Masters.RoleInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.domain.Entity.UserRoleModule
{
    public partial class UserRole
    {
        public long Id { get; set; }

        public long EmployeeId { get; set; }

        public int RoleId { get; set; }

        public bool? IsActive { get; set; }

        public string? Remark { get; set; }

        public DateTime? AssignedDateTime { get; set; }

        public DateTime? RemovedDateTime { get; set; }

        public long AssignedById { get; set; }

        public DateTime? RoleStartDate { get; set; }

        public long AddedById { get; set; }

        public DateTime? AddedDateTime { get; set; }

        public long? UpdatedById { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
    }

}
