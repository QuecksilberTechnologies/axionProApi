using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.EmployeeDTO
{
    public class EmployeeDTO
    {
        public long Id { get; set; }
        public long EmployeeDocumentId { get; set; }
        public string EmployementCode { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public DateOnly DateOfOnBoarding { get; set; }
        public DateOnly? DateOfExit { get; set; }
        public long? SpecializationId { get; set; }
        public int? DesignationId { get; set; }
        public int? EmployeeTypeId { get; set; }
        public int? DepartmentId { get; set; }
        public string OfficialEmail { get; set; } = null!;
        public bool HasPermanent { get; set; }
        public bool IsActive { get; set; }
        public int? FunctionalId { get; set; }
        public string? ReferalCode { get; set; }
        public long? AddedById { get; set; }
        public DateTime AddedDateTime { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
