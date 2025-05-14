using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class LoginCredential
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public string LoginId { get; set; } = null!;

    public string? Password { get; set; }

    public bool? HasFirstLogin { get; set; }

    public string? MacAddress { get; set; }

    public bool? IsActive { get; set; }

    public string? Remark { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int LoginDevice { get; set; }

    public string? IpAddressLocal { get; set; }

    public string? IpAddressPublic { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<RefreshToken1> RefreshToken1s { get; set; } = new List<RefreshToken1>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
