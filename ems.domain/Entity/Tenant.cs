using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class Tenant
{
    public long Id { get; set; }

    public string TenantName { get; set; } = null!;

    public string TenantCode { get; set; } = null!;

    public string TenantEmail { get; set; } = null!;

    public string? ContactPersonName { get; set; }

    public string? ContactNumber { get; set; }

    public string? Address { get; set; }

    public string? Country { get; set; }

    public string? LogoUrl { get; set; }

    public string? ThemeColor { get; set; }

    public string? VerificationToken { get; set; }

    public bool IsVerified { get; set; }

    public bool IsActive { get; set; }

    public int? SubscriptionPlanId { get; set; }
}
