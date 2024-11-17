using ems.domain.Common;
using ems.domain.Entity.EmployeeModule;
using ems.domain.Entity.RoleModulePermission;
using System;
using System.Collections.Generic;

namespace ems.domain.Entity.BasicMenuInfo;

public partial class BasicMenu : BaseEntity
{

    public int Id { get; set; }

    public string MenuName { get; set; } = null!;

    public string? MenuUrl { get; set; }

    public int? ParentMenuId { get; set; }

    public string Remark { get; set; } = null!;

    public byte[]? ImageIcon { get; set; }

    public bool IsActive { get; set; }

    public long? AddedById { get; set; }

    public DateTime AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual ICollection<EmployeeTypeBasicMenu> EmployeeTypeBasicMenus { get; set; } = new List<EmployeeTypeBasicMenu>();

    public virtual ICollection<BasicMenu> InverseParentMenu { get; set; } = new List<BasicMenu>();

    public virtual BasicMenu? ParentMenu { get; set; }
}

