using ems.domain.Common;
using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class Tenant :BaseEntity
{
    public long Id { get; set; }

    public int TenantIndustryId { get; set; }   
    public string CompanyName { get; set; } = null!;

    public string? TenantCode { get; set; }

    public string CompanyEmailDomain { get; set; } = null!;

    public string TenantEmail { get; set; } = null!;

    public string? ContactPersonName { get; set; }

    public string? ContactNumber { get; set; }

    public int CountryId { get; set; }

    public bool IsVerified { get; set; }
    public virtual TenantIndustry TenantIndustry { get; set; } = null!;

    public virtual ICollection<ForgotPasswordOTPDetail> ForgotPasswordRequests { get; set; } = new List<ForgotPasswordOTPDetail>();

    public virtual ICollection<Designation> Designations { get; set; } = new List<Designation>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

 //   public virtual ICollection<RequestType> RequestTypes { get; set; } = new List<RequestType>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<TenantEmailConfig> TenantEmailConfigs { get; set; } = new List<TenantEmailConfig>();

    public virtual ICollection<TenantEnabledModule> TenantEnabledModules { get; set; } = new List<TenantEnabledModule>();

    public virtual ICollection<TenantEnabledOperation> TenantEnabledOperations { get; set; } = new List<TenantEnabledOperation>();

    public virtual ICollection<TenantProfile> TenantProfiles { get; set; } = new List<TenantProfile>();
    public virtual ICollection<EmployeeManagerMapping> EmployeeManagerMappings { get; set; } = new List<EmployeeManagerMapping>();

    public virtual ICollection<TenantSubscription> TenantSubscriptions { get; set; } = new List<TenantSubscription>();
}
