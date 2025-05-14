using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class EmployeeEducation
{
    public int Id { get; set; }

    public long EmployeeId { get; set; }

    public string Degree { get; set; } = null!;

    public string InstituteName { get; set; } = null!;

    public string? Remark { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string GradeOrPercentage { get; set; } = null!;

    public string? GpaorPercentage { get; set; }

    public bool? EducationGap { get; set; }

    public string? ReasonOfEducationGap { get; set; }

    public bool IsActive { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
