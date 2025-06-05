using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class Tenant
{
    public long Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? TenantCode { get; set; }

    public string CompanyEmailDomain { get; set; } = null!;

    public string TenantEmail { get; set; } = null!;

    public string? ContactPersonName { get; set; }

    public string? ContactNumber { get; set; }

    public int CountryId { get; set; }

    public bool IsVerified { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<LoginCredential> LoginCredentials { get; set; } = new List<LoginCredential>();

    public virtual ICollection<TenantEmailConfig> TenantEmailConfigs { get; set; } = new List<TenantEmailConfig>();

    public virtual ICollection<TenantProfile> TenantProfiles { get; set; } = new List<TenantProfile>();

    
}
