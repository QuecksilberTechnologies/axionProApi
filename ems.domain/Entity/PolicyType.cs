using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class PolicyType
{
    public int Id { get; set; }

    public string PolicyName { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsSoftDelete { get; set; }

    public DateTime? SoftDeleteDateTime { get; set; }

   // public virtual ICollection<AccoumndationAllowancePolicyByDesignation> AccoumndationAllowancePolicyByDesignations { get; set; } = new List<AccoumndationAllowancePolicyByDesignation>();

    public virtual ICollection<LeavePolicyByDesignation> LeavePolicyByDesignations { get; set; } = new List<LeavePolicyByDesignation>();

 //   public virtual ICollection<MealAllowancePolicyByDesignation> MealAllowancePolicyByDesignations { get; set; } = new List<MealAllowancePolicyByDesignation>();

  //  public virtual ICollection<TravelAllowancePolicyByDesignation> TravelAllowancePolicyByDesignations { get; set; } = new List<TravelAllowancePolicyByDesignation>();
}
