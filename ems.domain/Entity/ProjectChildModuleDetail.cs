using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class ProjectChildModuleDetail
{
    public int Id { get; set; }

    public int SubModuleId { get; set; }

    public byte[]? IconImage { get; set; }

    public string ChildModuleName { get; set; } = null!;

    public string? ChildModuleUrl { get; set; }

    public bool IsActive { get; set; }

    public string? Remark { get; set; }

    public bool? IsOperational { get; set; }

    public virtual ICollection<RoleModuleAndPermission> RoleModuleAndPermissions { get; set; } = new List<RoleModuleAndPermission>();

    public virtual ProjectSubModuleDetail SubModule { get; set; } = null!;
}
