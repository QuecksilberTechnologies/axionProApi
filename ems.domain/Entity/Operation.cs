using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class Operation
{
    public int Id { get; set; }

    public string OperationName { get; set; } = null!;

    public string? Remark { get; set; }

    public bool IsActive { get; set; }

    public long? AddedById { get; set; }

    public DateTime AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdateDateTime { get; set; }

    public byte[]? IconImage { get; set; }

    public virtual ICollection<ModuleOperationMapping> ModuleOperationMappings { get; set; } = new List<ModuleOperationMapping>();
}
 
