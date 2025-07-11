using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{
  public  class CreateEmployeeByTenantAdminRequestDTO
    {
        [Required]
        public long TenantId { get; set; }
        [MaxLength(100)]      
        public required string OfficialEmail { get; set; }
        [Required]
        [MaxLength(50)]
        public required string EmployementCode { get; set; }
        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }
        [Required]        
        [MaxLength(100)]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }

        public  DateTime? DateOfOnBoarding { get; set; } 

        public int? DepartmentId { get; set; }    // Optional Dropdown

        public int? DesignationId { get; set; }   // Optional Dropdown

        [Required]
        public required int EmployeeTypeId { get; set; }   // Dropdown: e.g., Intern, Full-Time, Contractor

        [Required]
        public required bool HasPermanent { get; set; }    // Permanent employment flag

        [Required]
        public required bool IsActive { get; set; }        // Active flag
    }
}
