using System;
using System.Collections.Generic;

namespace ems.api.Models;

public partial class ProjectSubModuleDetail
{
    public int Id { get; set; }

    public int ModuleId { get; set; }

    public string SubModuleName { get; set; } = null!;

    public string SubModuleUrl { get; set; } = null!;

    public bool IsSubModuleDisplayInUi { get; set; }

    public bool IsActive { get; set; }

    public string? Remark { get; set; }

    public long AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual ProjectModuleDetail Module { get; set; } = null!;

    public virtual ICollection<RoleModuleAndPermission> RoleModuleAndPermissions { get; set; } = new List<RoleModuleAndPermission>();
}
