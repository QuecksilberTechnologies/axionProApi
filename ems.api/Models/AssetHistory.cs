﻿using System;
using System.Collections.Generic;

namespace ems.api.Models;

public partial class AssetHistory
{
    public int Id { get; set; }

    public int AssetId { get; set; }

    public long? EmployeeId { get; set; }

    public DateTime? AssignedDate { get; set; }

    public DateTime? ReturnedDate { get; set; }

    public int AssignmentStatusId { get; set; }

    public string AssetConditionAtAssign { get; set; } = null!;

    public string? AssetConditionAtReturn { get; set; }

    public string IdentificationMethod { get; set; } = null!;

    public string IdentificationValue { get; set; } = null!;

    public bool? IsScrapped { get; set; }

    public string? ScrapReason { get; set; }

    public long? ScrapApprovedBy { get; set; }

    public DateTime? ScrapDate { get; set; }

    public string? Remarks { get; set; }

    public long CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual AssignmentStatus AssignmentStatus { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual Employee? ScrapApprovedByNavigation { get; set; }
}
