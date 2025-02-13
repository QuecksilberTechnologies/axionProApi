using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class LeavePolicyByDesignation
{
    public int Id { get; set; }

    public int PolicyTypeId { get; set; }

    public int? DesignationId { get; set; }

    public int EmployeeTypeId { get; set; }

    public int LeaveTypeId { get; set; }

    public bool? IsCarriedForward { get; set; }

    public int? MaxConsecutiveLeave { get; set; }

    public int? NoOfLeave { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsSoftDelete { get; set; }

    public DateTime? SoftDeleteDateTime { get; set; }

    public int? ApplicableGenderId { get; set; }

    public bool? IsSandwichRuleApplied { get; set; }

    public virtual Gender? ApplicableGender { get; set; }

    public virtual Designation? Designation { get; set; }

    public virtual EmployeeType EmployeeType { get; set; } = null!;

    public virtual LeaveType LeaveType { get; set; } = null!;

    public virtual PolicyType PolicyType { get; set; } = null!;
}
