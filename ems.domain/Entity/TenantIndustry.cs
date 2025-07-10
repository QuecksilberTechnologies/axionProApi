using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.domain.Entity
{
    public partial class TenantIndustry
    {
        public int Id { get; set; }

        public string IndustryName { get; set; } = null!;

        public string? Description { get; set; }

        public string? Remark { get; set; }

        public bool IsActive { get; set; }

        public long? AddedById { get; set; }

        public DateTime? AddedDateTime { get; set; }

        public long? UpdatedById { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public bool? IsSoftDeted { get; set; }

        public long? DeletedById { get; set; }

        public DateTime? DeletedDateTime { get; set; }

        public virtual ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
    }

}
