using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Tenant
{
    public class TenantSubscriptionPlanRequestDTO
    {
        public long EmployeeId { get; set; }
        public int RoleId { get; set; }
        public int DesignationId { get; set; }
        public long? Id { get; set; }
        public long? TenantId { get; set; }
        public long? SubscriptionPlanId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsCancelled { get; set; }
        public string? Remark { get; set; }

    }
}
