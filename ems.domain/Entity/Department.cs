using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class Department
{
    public int Id { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public string? Remark { get; set; }

    public long? AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }
    public virtual ICollection<EmployeeManagerMapping> EmployeeManagerMappings { get; set; } = new List<EmployeeManagerMapping>();

}
