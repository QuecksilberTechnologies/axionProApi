using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

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

    public virtual ICollection<AttendanceHistory> AttendanceHistories { get; set; } = new List<AttendanceHistory>();
  

    public virtual ICollection<EmployeeDailyAttendance> EmployeeDailyAttendances { get; set; } = new List<EmployeeDailyAttendance>();

    public virtual ICollection<EmployeeStatusHistory> EmployeeStatusHistories { get; set; } = new List<EmployeeStatusHistory>();

    public virtual EmployeeType? EmployeeType { get; set; }
 
    public virtual ICollection<UserRole> UserRolesEmp { get; set; } = new List<UserRole>();


    public virtual ICollection<LoginCredential> LoginCredentials { get; set; } = new List<LoginCredential>();

    public virtual ICollection<UserAttendanceSetting> UserAttendanceSettings { get; set; } = new List<UserAttendanceSetting>();
}
