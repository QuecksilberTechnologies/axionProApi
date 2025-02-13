using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class LeaveType
{
    public int Id { get; set; }

    public string LeaveName { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<LeaveAllocation> LeaveAllocations { get; set; } = new List<LeaveAllocation>();

    public virtual ICollection<LeaveCarryForwardRule> LeaveCarryForwardRules { get; set; } = new List<LeaveCarryForwardRule>();

    public virtual ICollection<LeavePolicyByDesignation> LeavePolicyByDesignations { get; set; } = new List<LeavePolicyByDesignation>();
}
