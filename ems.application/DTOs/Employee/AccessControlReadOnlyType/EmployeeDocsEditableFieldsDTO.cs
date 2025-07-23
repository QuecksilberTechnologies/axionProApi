﻿using ems.application.Common.Attributes;

namespace ems.application.DTOs.Employee.AccessControlReadOnlyType
{
    public class EmployeeDocsEditableFieldsDTO
    {
        [AccessControl(ReadOnly = true)]
        public long Id { get; set; }

        [AccessControl(ReadOnly = true)]
        public long EmployeeId { get; set; }

        [AccessControl(ReadOnly = true)]
        public string? AadhaarNumber { get; set; }

        [AccessControl(ReadOnly = true)]
        public string? PanNumber { get; set; }

        [AccessControl(ReadOnly = true)]
        public string? PassportNumber { get; set; }

        [AccessControl(ReadOnly = true)]
        public string? DrivingLicenseNumber { get; set; }

        [AccessControl(ReadOnly = true)]
        public string? VoterId { get; set; }

        [AccessControl(ReadOnly = true)]
        public string? BloodGroup { get; set; }

        [AccessControl(ReadOnly = true)]
        public string? MaritalStatus { get; set; }

        [AccessControl(ReadOnly = true)]
        public string? Nationality { get; set; }

        [AccessControl(ReadOnly = true)]
        public string? EmergencyContactName { get; set; }

        [AccessControl(ReadOnly = true)]
        public string? EmergencyContactNumber { get; set; }

        [AccessControl(ReadOnly = true)]
        public bool? IsActive { get; set; }

        [AccessControl(ReadOnly = true)]
        public bool? IsSoftDeleted { get; set; }

        [AccessControl(ReadOnly = true)]
        public long? AddedById { get; set; }

        [AccessControl(ReadOnly = true)]
        public DateTime? AddedDateTime { get; set; }

        [AccessControl(ReadOnly = true)]
        public long? UpdatedById { get; set; }

        [AccessControl(ReadOnly = true)]
        public DateTime? UpdatedDateTime { get; set; }

        [AccessControl(ReadOnly = true)]
        public long? DeletedById { get; set; }

        [AccessControl(ReadOnly = true)]
        public DateTime? DeletedDateTime { get; set; }

        [AccessControl(ReadOnly = true)]
        public long? InfoVerifiedById { get; set; }

        [AccessControl(ReadOnly = true)]
        public DateTime? InfoVerifiedDateTime { get; set; }

        [AccessControl(ReadOnly = true)]
        public bool? IsInfoVerified { get; set; }
    }
}
