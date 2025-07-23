using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class EmsModuleDetail
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

    public virtual ICollection<EmsSubModuleDetail> ProjectSubModuleDetails { get; set; } = new List<EmsSubModuleDetail>();
}
