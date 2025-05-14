using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class ProjectModuleDetail
{
    public int Id { get; set; }

    public string? ModuleName { get; set; }

    public string? ModuleUrl { get; set; }

    public bool? IsActive { get; set; }

    public string? Remark { get; set; }

    public byte[]? IconImage { get; set; }

    public virtual ICollection<ProjectSubModuleDetail> ProjectSubModuleDetails { get; set; } = new List<ProjectSubModuleDetail>();
}
