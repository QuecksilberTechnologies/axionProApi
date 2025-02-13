using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class Gender
{
    public int Id { get; set; }

    public string GenderName { get; set; } = null!;

    public virtual ICollection<LeavePolicyByDesignation> LeavePolicyByDesignations { get; set; } = new List<LeavePolicyByDesignation>();
}
