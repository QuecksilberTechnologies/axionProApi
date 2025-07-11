using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{

    public class CreateEmployeeRequestDTO
    {
        public required long TenantId { get; set; }

        public string? EmployeeDocumentId { get; set; } // optional

        public required string EmployementCode { get; set; }

        public required string LastName { get; set; }

        public required string MiddleName { get; set; }

        public required string FirstName { get; set; }

        public required DateTime DateOfBirth { get; set; }

        public required DateTime DateOfOnBoarding { get; set; }

        public DateTime? DateOfExit { get; set; } // optional

        public long DesignationId { get; set; } // optional if 0 (use validation)

        public required long EmployeeTypeId { get; set; }

        public int? DepartmentId { get; set; } // optional

        public int? RoleId { get; set; } // optional

        public string? OfficialEmail { get; set; } // optional

        public required bool? HasPermanent { get; set; }

        public required bool IsActive { get; set; }

        public long? FunctionalId { get; set; } // optional

        public long? ReferalId { get; set; } // optional

        public string? Remark { get; set; } // optional
    }

    // Add more fields as per your requirement




}

