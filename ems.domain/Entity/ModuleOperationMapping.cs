using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class ModuleOperationMapping
{
    public int Id { get; set; }
   

    public int OperationId { get; set; }

    public string? DisplayName { get; set; }

    public string? PageUrl { get; set; }

    public string? IconUrl { get; set; }

    public bool? IsCommonItem { get; set; }

    public bool? IsOperational { get; set; }

    public int? Priority { get; set; }

    public string? Remark { get; set; }

    public bool? IsActive { get; set; }

    public int? AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public int? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }
 
    public virtual Operation Operation { get; set; } = null!;
}
