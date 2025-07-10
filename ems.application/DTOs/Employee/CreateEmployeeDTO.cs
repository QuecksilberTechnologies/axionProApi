using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{

    public class CreateEmployeeRequestDTO
    {
        public class CreateEmployeeDTO
        {
            public long TenantId { get; set; }
            public string? EmployeeDocumentId { get; set; }
            public string? EmployementCode { get; set; }

            public string? LastName { get; set; }
            public string? MiddleName { get; set; }
            public string? FirstName { get; set; }

            public DateTime? DateOfBirth { get; set; }
            public DateTime? DateOfOnBoarding { get; set; }
            public DateTime? DateOfExit { get; set; }

            public long? SpecializationId { get; set; }
            public long? DesignationId { get; set; }
            public long? EmployeeTypeId { get; set; }
            public long? DepartmentId { get; set; }

            public string? OfficialEmail { get; set; }

            public bool? HasPermanent { get; set; }
          
             public bool IsActive { get; set; }

            public long? FunctionalId { get; set; }
            public long? ReferalId { get; set; }

            public string? Remark { get; set; }

            // Audit Fields
            public long? AddedById { get; set; }
            public DateTime? AddedDateTime { get; set; }

            public long? UpdatedById { get; set; }
            public DateTime? UpdatedDateTime { get; set; }

            public long? DeletedById { get; set; }
            public DateTime? DeletedDateTime { get; set; }

           // public bool IsSoftDeleted { get; set; }
        }

        // Add more fields as per your requirement
    }



}

