using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class ProjectSubModuleDetail
{
    public int Id { get; set; }

    public int ModuleId { get; set; }

    public string SubModuleName { get; set; } = null!;

    public string SubModuleUrl { get; set; } = null!;

    public bool IsSubModuleDisplayInUi { get; set; }

    public bool IsActive { get; set; }

    public string? Remark { get; set; }

    public virtual ProjectModuleDetail Module { get; set; } = null!;
    public byte[]? IconImage { get; set; }
    public virtual ICollection<ProjectChildModuleDetail> ProjectChildModuleDetails { get; set; } = new List<ProjectChildModuleDetail>();
}
