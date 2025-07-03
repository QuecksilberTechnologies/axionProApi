﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.SubscriptionModule
{
    public class GetTenantSubscriptionDetailResponsDTO 
    {
        public long? TenantId { get; set; }
        public string? PlanName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }

    }
}
