using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class EmployeeType
{
    public int Id { get; set; }

    public string? TypeName { get; set; }

    public string? Description { get; set; }

    public string? Remark { get; set; }

    public bool? IsActive { get; set; }

    public long? AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual ICollection<AccoumndationAllowancePolicyByDesignation> AccoumndationAllowancePolicyByDesignations { get; set; } = new List<AccoumndationAllowancePolicyByDesignation>();

    public virtual ICollection<EmployeeStatusHistory> EmployeeStatusHistoryNewEmployeeTypes { get; set; } = new List<EmployeeStatusHistory>();

    public virtual ICollection<EmployeeStatusHistory> EmployeeStatusHistoryOldEmployeeTypes { get; set; } = new List<EmployeeStatusHistory>();

    public virtual ICollection<EmployeeTypeBasicMenu> EmployeeTypeBasicMenus { get; set; } = new List<EmployeeTypeBasicMenu>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<LeavePolicyByDesignation> LeavePolicyByDesignations { get; set; } = new List<LeavePolicyByDesignation>();

    public virtual ICollection<MealAllowancePolicyByDesignation> MealAllowancePolicyByDesignations { get; set; } = new List<MealAllowancePolicyByDesignation>();

    public virtual ICollection<TravelAllowancePolicyByDesignation> TravelAllowancePolicyByDesignations { get; set; } = new List<TravelAllowancePolicyByDesignation>();
}
