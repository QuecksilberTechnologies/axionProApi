using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class EmployeeDependent
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public string DependentName { get; set; } = null!;

    public string Relation { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public bool IsCoveredInPolicy { get; set; }

    public string? Remark { get; set; }

    public string? Description { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual Employee Employee { get; set; } = null!;
    public bool? IsActive { get; set; }
    public long? AddedById { get; set; }
    public DateTime? AddedDateTime { get; set; }
    public long? UpdatedById { get; set; }
    public bool? IsSoftDeleted { get; set; }
    public long? InfoVerifiedById { get; set; }

    public bool? IsInfoVerified { get; set; }

    public DateTime? InfoVerifiedDateTime { get; set; }

    public long? DeletedById { get; set; }
    public DateTime? DeletedDateTime { get; set; }

}
