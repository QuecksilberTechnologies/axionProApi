using System;
using System.Collections.Generic;

namespace ems.api.Models;

public partial class EmployeePolicy
{
    public int Id { get; set; }

    public long? EmployeeId { get; set; }

    public int? PolicyId { get; set; }

    public DateOnly? AssignedDate { get; set; }

    public string? Description { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public long? ApprovedById { get; set; }

    public bool IsActive { get; set; }
}
