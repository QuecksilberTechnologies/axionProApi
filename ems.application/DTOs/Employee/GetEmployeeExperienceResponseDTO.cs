using ems.application.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{
    public class GetEmployeeExperienceResponseDTO
    {
        public long? Id { get; set; }
        public long? EmployeeId { get; set; }
        public string? CompanyName { get; set; }
        public string? JobTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ReasonForLeaving { get; set; }
        public string? Remark { get; set; }
        public bool? IsExperienceVerified { get; set; }
        public long? ExperienceVerificationBy { get; set; }
        public bool? IsSoftDeleted { get; set; }
        public int? ExperienceTypeId { get; set; }
        public string? Location { get; set; }
        public decimal? CTC { get; set; }
        public string? ReportingManagerName { get; set; }
        public string? ReportingManagerNumber { get; set; }
        public string? ReportingManagerEmail { get; set; }
        public string? WorkedWithName { get; set; }
        public string? WorkedWithContactNumber { get; set; }
        public string? WorkedWithDesignation { get; set; }
        public string? ExperienceLetterPath { get; set; }
        public string? Comment { get; set; }
        public long? AddedById { get; set; }
        public DateTime? AddedDateTime { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public long? DeletedById { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public bool? IsExperienceVerifiedByMail { get; set; }
        public bool? IsExperienceVerifiedByCall { get; set; }
        public long? InfoVerifiedById { get; set; }
        public bool? IsInfoVerified { get; set; }
        public DateTime? InfoVerifiedDateTime { get; set; }
        public DateTime? ExperienceVerificationDateTime { get; set; }
        public bool? IsEditAllowed { get; set; }
        public bool? IsActive { get; set; }
    }



}
