using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.SubscriptionModule
{
    public class SubscriptionPlanResponseDTO
    {
        public int? Id { get; set; }

        public string? PlanName { get; set; } = null!;

        public int? MaxUsers { get; set; }

        public decimal? PerDayPrice { get; set; }

        public decimal? MonthlyPrice { get; set; }

        public decimal? YearlyPrice { get; set; }

        public bool? IsActive { get; set; }


    }

}
