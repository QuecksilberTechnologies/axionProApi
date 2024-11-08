using ems.domain.Common;
using ems.domain.Entity.UserCredential;
using ems.domain.Entity.UserRoleModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.domain.Entity.EmployeeModule
{
    // ems.domain/Entity/Employee.cs
    [Table("Employee")]
 
    public partial class Employee : BaseEntity
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

        public string? ReferalCode { get; set; }

        public string? Remark { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public virtual ICollection<LoginCredential> LoginCredentials { get; set; } = new List<LoginCredential>();

    }



}
