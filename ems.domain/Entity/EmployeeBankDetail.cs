using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class EmployeeBankDetail
{
    public int Id { get; set; }

    public long? EmployeeId { get; set; }

    public string? BankName { get; set; }

    public string? AccountNumber { get; set; }

    public string? Ifsccode { get; set; }

    public string? BranchName { get; set; }

    public string? AccountType { get; set; }

    public string? Upiid { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsPrimaryAccount { get; set; }

    public bool? IsEditAllowed { get; set; }

    public long? AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public long? DeletedById { get; set; }

    public DateTime? DeletedDateTime { get; set; }

    public bool? IsSoftDeleted { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public bool? IsInfoVerified { get; set; }

    public long? InfoVerifiedById { get; set; }

    public DateTime? InfoVerifiedDateTime { get; set; }

    public virtual Employee? Employee { get; set; }
}
