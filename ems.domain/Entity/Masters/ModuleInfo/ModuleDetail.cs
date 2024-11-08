using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.domain.Entity.Masters.ModuleInfo
{

    public partial class ModuleDetail
    {
        public int Id { get; set; }

        public string ModuleName { get; set; } = null!;

        public string? ModuleUrl { get; set; }

        public int? ParentModuleId { get; set; }

        public bool IsModuleDisplayInUi { get; set; }

        public bool IsSubModule { get; set; }

        public bool IsActive { get; set; }

        public string? Remark { get; set; }

        public long AddedById { get; set; }

        public DateTime AddedDateTime { get; set; }

        public long? UpdatedById { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public virtual ICollection<ModuleDetail> InverseParentModule { get; set; } = new List<ModuleDetail>();

        public virtual ModuleDetail? ParentModule { get; set; }

        //public virtual ICollection<RolesPermission> RolesPermissions { get; set; } = new List<RolesPermission>();
    }

}
