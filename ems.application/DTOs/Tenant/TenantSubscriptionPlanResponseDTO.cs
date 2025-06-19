using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Tenant
{
    public class TenantSubscriptionPlanResponseDTO
    {

        public long Id { get; set; }
        public long TenantId { get; set; }       
        public long SubscriptionPlanId { get; set; }       
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsCancelled { get; set; }
        public string? Remark { get; set; }
        public DateTime AddedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }


}
