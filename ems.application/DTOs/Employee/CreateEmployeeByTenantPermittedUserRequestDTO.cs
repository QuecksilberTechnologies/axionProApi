using ems.application.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{

    public class CreateEmployeeByTenantPermittedUserRequestDTO
    {
        [Required]
        [AccessControl(ReadOnly = false)]
        public long TenantId { get; set; }

        [Required]
        [AccessControl(ReadOnly = false)]
        public int EmployeeDocumentId { get; set; }

        [Required]
        [AccessControl(ReadOnly = false)]
        [MaxLength(50)]
        public required string EmployementCode { get; set; }

        [Required]
        [AccessControl(ReadOnly = false)]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [AccessControl(ReadOnly = false)]
        [MaxLength(100)]
        public string? MiddleName { get; set; }

        [Required]
        [AccessControl(ReadOnly = false)]
        [MaxLength(100)]
        public required string LastName { get; set; }

        [Required]
        [AccessControl(ReadOnly = false)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [AccessControl(ReadOnly = false)]
        public DateTime? DateOfOnBoarding { get; set; }

        [AccessControl(ReadOnly = false)]
        public DateTime? DateOfExit { get; set; }

        [AccessControl(ReadOnly = false)]
        public int? DesignationId { get; set; }

        [AccessControl(ReadOnly = false)]
        public int? DepartmentId { get; set; }

        [Required]
        [AccessControl(ReadOnly = false)]
        public int? EmployeeTypeId { get; set; }

        [Required]
        [AccessControl(ReadOnly = false)]
        public required bool HasPermanent { get; set; }

        [Required]
        [AccessControl(ReadOnly = false)]
        public required bool IsActive { get; set; }

        [Required]
        [AccessControl(ReadOnly = false)]
        [EmailAddress]
        public required string OfficialEmail { get; set; }

        [AccessControl(ReadOnly = false)]
        public int? FunctionalId { get; set; }

        [AccessControl(ReadOnly = false)]
        public int? ReferalId { get; set; }

        [AccessControl(ReadOnly = false)]
        public string? Remark { get; set; }

        [AccessControl(ReadOnly = false)]
        public string? Description { get; set; }

        // ✅ Info Verification Fields
        [AccessControl(ReadOnly = false)]
        public DateTime? InfoVerifiedDateTime { get; set; }

        [AccessControl(ReadOnly = false)]
        public long? InfoVerifiedById { get; set; }

        [AccessControl(ReadOnly = false)]
        public bool? IsInfoVerified { get; set; }
         
        [AccessControl(ReadOnly = false)]
        public bool? IsDeleted { get; set; }

        [AccessControl(ReadOnly = false)]
        public long? DeletedById { get; set; }

        [AccessControl(ReadOnly = false)]
        public DateTime? DeletedDateTime { get; set; }
    }


}
