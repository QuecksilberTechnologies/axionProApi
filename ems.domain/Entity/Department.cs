using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class Department
{
    public int Id { get; set; }

    public long? TenantId { get; set; }

    public int? TenantIndustryId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }
    public bool? IsExecutiveOffice { get; set; }

    public string? Remark { get; set; }

    public long? AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public bool? IsSoftDeleted { get; set; }

    public long? DeletedById { get; set; }

    public DateTime? DeletedDateTime { get; set; }
    public virtual ICollection<Designation> Designations { get; set; } = new List<Designation>();

    public virtual ICollection<EmployeeManagerMapping> EmployeeManagerMappings { get; set; } = new List<EmployeeManagerMapping>();

    public virtual TenantIndustry? TenantIndustry { get; set; }
}

