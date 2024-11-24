using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.domain.Entity
{
    public partial class ProjectModuleDetail
    {
        public int Id { get; set; }

        public string? ModuleName { get; set; }

        public string? ModuleUrl { get; set; }

        public bool? IsActive { get; set; }

        public string? Remark { get; set; }

        public long? AddedById { get; set; }

        public DateTime? AddedDateTime { get; set; }

        public long? UpdatedById { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public virtual ICollection<ProjectSubModuleDetail> ProjectSubModuleDetails { get; set; } = new List<ProjectSubModuleDetail>();

        public virtual ICollection<RoleModuleAndPermission> RoleModuleAndPermissions { get; set; } = new List<RoleModuleAndPermission>();

    }




}
