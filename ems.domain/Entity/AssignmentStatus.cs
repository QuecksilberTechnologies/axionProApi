using ems.domain.Common;
using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class AssignmentStatus :BaseEntity
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public string? Description { get; set; }

 

    public virtual ICollection<AssetAssignment> AssetAssignments { get; set; } = new List<AssetAssignment>();

    public virtual ICollection<AssetHistory> AssetHistories { get; set; } = new List<AssetHistory>();
}
