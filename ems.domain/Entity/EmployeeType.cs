using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class EmployeeType
{
    public int Id { get; set; }

    public string? TypeName { get; set; }

    public int? RoleId { get; set; }

    public string? Description { get; set; }

    public string? Remark { get; set; }

    public bool? IsActive { get; set; }

    public long? AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual ICollection<EmployeeStatusHistory> EmployeeStatusHistoryNewEmployeeTypes { get; set; } = new List<EmployeeStatusHistory>();

    public virtual ICollection<EmployeeStatusHistory> EmployeeStatusHistoryOldEmployeeTypes { get; set; } = new List<EmployeeStatusHistory>();

    public virtual ICollection<EmployeeTypeBasicMenu> EmployeeTypeBasicMenus { get; set; } = new List<EmployeeTypeBasicMenu>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Role? Role { get; set; }
}
