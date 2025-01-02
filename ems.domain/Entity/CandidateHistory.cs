using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.domain.Entity
{
    public partial class CandidateHistory
    {
        public long Id { get; set; }

        public long CandidateId { get; set; }

        public string Status { get; set; } = null!;

        public string Reason { get; set; } = null!;

        public long AddedById { get; set; }

        public DateTime? ReapplyAllowedAfter { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public virtual Candidate Candidate { get; set; } = null!;
    }

}
