using System;
using System.Collections.Generic;

namespace ems.api.Models;

public partial class Employee
{
    public long Id { get; set; }

    public int EmployeeDocumentId { get; set; }

    public string EmployementCode { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string FirstName { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public DateOnly? DateOfOnBoarding { get; set; }

    public DateOnly? DateOfExit { get; set; }

    public int? SpecializationId { get; set; }

    public int? DesignationId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? DepartmentId { get; set; }

    public string? OfficialEmail { get; set; }

    public bool HasPermanent { get; set; }

    public bool IsActive { get; set; }

    public int? FunctionalId { get; set; }

    public int? ReferalId { get; set; }

    public string? Remark { get; set; }

    public long AddedById { get; set; }

    public DateTime AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual ICollection<AssetAssignment> AssetAssignments { get; set; } = new List<AssetAssignment>();

    public virtual ICollection<AssetHistory> AssetHistoryEmployees { get; set; } = new List<AssetHistory>();

    public virtual ICollection<AssetHistory> AssetHistoryScrapApprovedByNavigations { get; set; } = new List<AssetHistory>();

    public virtual ICollection<AttendanceHistory> AttendanceHistories { get; set; } = new List<AttendanceHistory>();

    public virtual Designation? Designation { get; set; }

    public virtual ICollection<EmployeeCategorySkill> EmployeeCategorySkills { get; set; } = new List<EmployeeCategorySkill>();

    public virtual ICollection<EmployeeDailyAttendance> EmployeeDailyAttendances { get; set; } = new List<EmployeeDailyAttendance>();

    public virtual ICollection<EmployeeDependent> EmployeeDependents { get; set; } = new List<EmployeeDependent>();

    public virtual ICollection<EmployeeEducation> EmployeeEducations { get; set; } = new List<EmployeeEducation>();

    public virtual ICollection<EmployeeExperience> EmployeeExperiences { get; set; } = new List<EmployeeExperience>();

    public virtual ICollection<EmployeePersonalDetail> EmployeePersonalDetails { get; set; } = new List<EmployeePersonalDetail>();

    public virtual ICollection<EmployeeStatusHistory> EmployeeStatusHistories { get; set; } = new List<EmployeeStatusHistory>();

    public virtual EmployeeType? EmployeeType { get; set; }

    public virtual ICollection<LeaveAllocation> LeaveAllocations { get; set; } = new List<LeaveAllocation>();

    public virtual ICollection<LoginCredential> LoginCredentials { get; set; } = new List<LoginCredential>();

    public virtual ICollection<UserAttendanceSetting> UserAttendanceSettings { get; set; } = new List<UserAttendanceSetting>();
}
