using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class EmployeeExperience
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string JobTitle { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? ReasonForLeaving { get; set; }

    public string? Remark { get; set; }

    public bool? IsVerified { get; set; }

    public long? VerifiedBy { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
