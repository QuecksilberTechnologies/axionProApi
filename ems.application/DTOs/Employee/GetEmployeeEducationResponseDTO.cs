using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{
    public class GetEmployeeEducationResponseDTO
    {

        public int Id { get; set; }

        public long? EmployeeId { get; set; }

        public string? Degree { get; set; }

        public string? InstituteName { get; set; }

        public string? Remark { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? GradeOrPercentage { get; set; }

        public string? GPAOrPercentage { get; set; }

        public bool? EducationGap { get; set; }

        public string? ReasonOfEducationGap { get; set; }

        public long? AddedById { get; set; }

        public DateTime? AddedDateTime { get; set; }

        public long? UpdatedById { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public long? DeletedById { get; set; }

        public DateTime? DeletedDateTime { get; set; }

        public bool? IsSoftDeleted { get; set; }

        public long? InfoVerifiedById { get; set; }

        public bool? IsInfoVerified { get; set; }

        public DateTime? InfoVerifiedDateTime { get; set; }

        public bool? IsEditAllowed { get; set; }

        public bool? IsActive { get; set; }
    }


}
