using ems.domain.Common;
using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class Employee :BaseEntity
{
    public long Id { get; set; }

    public long? TenantId { get; set; }

    public int? EmployeeDocumentId { get; set; }

    public string? EmployementCode { get; set; } 

    public string? LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? FirstName { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public DateTime? DateOfOnBoarding { get; set; }

    public DateTime? DateOfExit { get; set; }

   // public int? SpecializationId { get; set; }

    public int? DesignationId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? DepartmentId { get; set; }

    public string? OfficialEmail { get; set; } 

    public bool? HasPermanent { get; set; }  
    public int? FunctionalId { get; set; }

    public int? ReferalId { get; set; }

    public string? Remark { get; set; } 
    public string? Description { get; set; } 

    public DateTime? InfoVerifiedDateTime { get; set; }
    public long? InfoVerifiedById { get; set; }
    public bool? IsInfoVerified { get; set; }
    public bool? IsEditAllowed { get; set; }
    public virtual Designation? Designation { get; set; }
    public virtual EmployeeType? EmployeeType { get; set; }
    public virtual Tenant Tenant { get; set; } = null!;

    public virtual ICollection<AssetAssignment> AssetAssignments { get; set; } = new List<AssetAssignment>();

    public virtual ICollection<AssetHistory> AssetHistoryEmployees { get; set; } = new List<AssetHistory>();

    public virtual ICollection<AssetHistory> AssetHistoryScrapApprovedByNavigations { get; set; } = new List<AssetHistory>();

    public virtual ICollection<AttendanceHistory> AttendanceHistories { get; set; } = new List<AttendanceHistory>();
    public virtual ICollection<EmployeeManagerMapping> EmployeeManagerMappings { get; set; } = new List<EmployeeManagerMapping>();
    public virtual ICollection<EmployeeManagerMapping> EmployeeManagerMappingEmployees { get; set; } = new List<EmployeeManagerMapping>();
    public virtual ICollection<EmployeeManagerMapping> EmployeeManagerMappingManagers { get; set; } = new List<EmployeeManagerMapping>();
 
    public virtual ICollection<EmployeeBankDetail> EmployeeBankDetails { get; set; } = new List<EmployeeBankDetail>();

    public virtual ICollection<EmployeeCategorySkill> EmployeeCategorySkills { get; set; } = new List<EmployeeCategorySkill>();

    public virtual ICollection<EmployeeDailyAttendance> EmployeeDailyAttendances { get; set; } = new List<EmployeeDailyAttendance>();

    public virtual ICollection<EmployeeDependent> EmployeeDependents { get; set; } = new List<EmployeeDependent>();

    public virtual ICollection<EmployeeEducation> EmployeeEducations { get; set; } = new List<EmployeeEducation>();

    public virtual ICollection<EmployeeExperience> EmployeeExperiences { get; set; } = new List<EmployeeExperience>();

    public virtual ICollection<EmployeePersonalDetail> EmployeePersonalDetails { get; set; } = new List<EmployeePersonalDetail>();

    public virtual ICollection<EmployeeStatusHistory> EmployeeStatusHistories { get; set; } = new List<EmployeeStatusHistory>();

  
    public virtual ICollection<LeaveAllocation> LeaveAllocations { get; set; } = new List<LeaveAllocation>();
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public virtual ICollection<LoginCredential> LoginCredentials { get; set; } = new List<LoginCredential>();

    public virtual ICollection<UserAttendanceSetting> UserAttendanceSettings { get; set; } = new List<UserAttendanceSetting>();
}
