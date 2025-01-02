using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.domain.Entity
{
    public partial class InterviewSchedule
    {
        public long Id { get; set; }

        public long CandidateId { get; set; }

        public DateTime ScheduledDateTime { get; set; }

        public long InterviewerId { get; set; }

        public string InterviewMode { get; set; } = null!;

        public string? Status { get; set; }

        public string? Remarks { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public virtual Candidate Candidate { get; set; } = null!;

        public virtual ICollection<InterviewFeedback> InterviewFeedbacks { get; set; } = new List<InterviewFeedback>();
    }

}
