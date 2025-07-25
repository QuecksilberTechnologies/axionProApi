﻿using ems.domain.Common;
using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class Role :BaseEntity
{
    public int Id { get; set; }

    public long? TenantId { get; set; }

    public string? RoleName { get; set; }

    public string? RoleType { get; set; }

    public bool? IsSystemDefault { get; set; } =false;
    public string? Remark { get; set; }
    public string? RoleCode { get; set; }
  
    public virtual Tenant Tenant { get; set; } = null!;

    public virtual ICollection<RoleModuleAndPermission> RoleModuleAndPermissions { get; set; } = new List<RoleModuleAndPermission>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

}

