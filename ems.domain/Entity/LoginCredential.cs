using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class LoginCredential
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public string? LoginId { get; set; }

    public string? Password { get; set; }

    public bool? HasFirstLogin { get; set; }

    public string? MacAddress { get; set; }

    public string? IpAddress { get; set; }

    public bool? IsActive { get; set; }

    public string? Remark { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int LoginDevice { get; set; }

    public long? AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
