using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class UserRole
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public int RoleId { get; set; }

    public bool? IsActive { get; set; }

    public string? Remark { get; set; }

    public DateTime? AssignedDateTime { get; set; }

    public DateTime? RemovedDateTime { get; set; }

    public long AssignedById { get; set; }

    public DateTime? RoleStartDate { get; set; }

    public long AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }
    public virtual Role? role { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }
}
