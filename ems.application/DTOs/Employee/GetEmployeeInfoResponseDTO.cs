using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{

    

   public class GetEmployeeInfoResponseDTO
    {
        public long TenantId { get; set; }
        public long EmployeeDocumentId { get; set; }
        public string EmployementCode { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfOnBoarding { get; set; }
        public DateTime? DateOfExit { get; set; }
        public int DesignationId { get; set; }
        public int EmployeeTypeId { get; set; }
        public int DepartmentId { get; set; }
        public string OfficialEmail { get; set; } = string.Empty;
        public bool HasPermanent { get; set; }
        public bool IsActive { get; set; }
        public bool IsEditAllowed { get; set; }
        public int FunctionalId { get; set; }
        public long? ReferalId { get; set; }
        public string? Remark { get; set; }


        // Audit Fields
        public long? AddedById { get; set; }
        public DateTime? AddedDateTime { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public long? DeletedById { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public bool IsSoftDeleted { get; set; }
    }
}
