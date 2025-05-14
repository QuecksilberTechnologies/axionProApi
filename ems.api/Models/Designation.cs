using System;
using System.Collections.Generic;

namespace ems.api.Models;

public partial class Designation
{
    public int Id { get; set; }

    public string DesignationName { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public int? AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public int? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual ICollection<AccoumndationAllowancePolicyByDesignation> AccoumndationAllowancePolicyByDesignations { get; set; } = new List<AccoumndationAllowancePolicyByDesignation>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<LeavePolicyByDesignation> LeavePolicyByDesignations { get; set; } = new List<LeavePolicyByDesignation>();

    public virtual ICollection<MealAllowancePolicyByDesignation> MealAllowancePolicyByDesignations { get; set; } = new List<MealAllowancePolicyByDesignation>();

    public virtual ICollection<TravelAllowancePolicyByDesignation> TravelAllowancePolicyByDesignations { get; set; } = new List<TravelAllowancePolicyByDesignation>();
}
