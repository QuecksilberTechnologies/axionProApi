using ems.domain.Common;
using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class AssetType :BaseEntity
{
    public int Id { get; set; }

    public long TenantId { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }
 

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}
