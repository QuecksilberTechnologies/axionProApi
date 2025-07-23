﻿using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class EmployeeTypeBasicMenu
{
    public int Id { get; set; }

    public int BasicMenuId { get; set; }

    public int EmployeeTypeId { get; set; }

    public int? ForPlatform { get; set; }

    public bool IsMenuDisplayInUi { get; set; }

    public bool IsDisplayable { get; set; }

    public bool IsActive { get; set; }

    public bool HasAccess { get; set; }

    public long? AddedById { get; set; }

    public DateTime AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual BasicMenu BasicMenu { get; set; } = null!;

    public virtual EmployeeType EmployeeType { get; set; } = null!;
}
