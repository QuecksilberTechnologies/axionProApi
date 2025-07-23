using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{
    public class GetEmployeePersonalDetailResponseDTO
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public string? AadhaarNumber { get; set; }
        public string? PanNumber { get; set; }
        public string? PassportNumber { get; set; }
        public string? DrivingLicenseNumber { get; set; }
        public string? VoterId { get; set; }
        public string? BloodGroup { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Nationality { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public bool IsActive { get; set; }
        public bool? IsSoftDeleted { get; set; }
        public long? AddedById { get; set; }
        public DateTime? AddedDateTime { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public long? DeletedById { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public long? InfoVerifiedById { get; set; }
        public DateTime? InfoVerifiedDateTime { get; set; }
        public bool? IsInfoVerified { get; set; }
    }
}
